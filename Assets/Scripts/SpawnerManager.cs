using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }
    public GameObject[] ParalaxBgs;
    private GameObject playerObject;
    public List<GameObject> enemies;
    public TextMeshProUGUI score;
    public TextMeshProUGUI stage;
    private int scoreInt;
    private float displayedScore = 0f;
    private int totalKills = 0;
    [SerializeField] private GameObject warningObj;

    [SerializeField] private float minX = -11f;
    [SerializeField] private float maxX = 40f;
    [SerializeField] private float yval = -2f;

    private int maxEnemies = 3;

    public GameObject[] runes;
    private int spawned = 0;
    public List<GameObject> activeEnemies = new List<GameObject>();
    private bool isSpawning = false;
    [SerializeField] private GameObject powerup;

    bool bossBattle = false;
    int scoresToNextStage = 2000;
    int nextStageAt = 100;
    [SerializeField] private GameObject bossPrefab;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartWave()
    {
        if (!isSpawning)
            StartCoroutine(SpawnEnemies());
    }
    private void Start()
    {
        score.text = scoreInt.ToString();
        playerObject = GameObject.FindWithTag("Player");
        stage.text = $"Incoming danger at {nextStageAt} points!";
    }
    private void Update()
    {
        spawned = activeEnemies.Count;
        displayedScore = Mathf.Lerp(displayedScore, scoreInt, Time.deltaTime * 5f);
        score.text = Mathf.RoundToInt(displayedScore).ToString();
    }
    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        while (activeEnemies.Count < maxEnemies)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), yval, 0f);

            GameObject warning = Instantiate(warningObj, spawnPos, Quaternion.identity);
            /*LeanTween.scale(warning, new Vector3(0.8f, 1f, 2f), 0.4f)
                .setEase(LeanTweenType.easeInOutSine)
                .setLoopPingPong();*/
            //warning.GetComponent<Animator>().Play("spawn");
            Destroy(warning, 1.5f);

            yield return new WaitForSeconds(1.4f);

            GameObject enemy = Instantiate(GetRandomEnemy(), spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().player = playerObject;
            activeEnemies.Add(enemy);
        }

        isSpawning = false;
    }

    private GameObject GetRandomEnemy()
    {
        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("Enemies list is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, enemies.Count);
        return enemies[randomIndex];
    }

    public void OnEnemyKilled(GameObject enemy, bool boss = false)
    {
        AudioManager.instance.Play("enemyDeath");
        
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }

        if (boss)
        {
            nextStageAt += scoresToNextStage;
            totalKills++;
            scoreInt += 100;
            maxEnemies++;
            SpawnRune();
            bossBattle = false;
            StartCoroutine(SpawnEnemies());
            stage.text = $"Incoming danger at {nextStageAt} points!";
            Debug.Log("BOSSSSSS");
            for(int i = 0; i<ParalaxBgs.Length; i++)
            {
                Debug.Log("AAAAAAAA");
                ParalaxBgs[i].GetComponent<ParalaxEffect>().Changebg();
            }
        }
        else
        {
            totalKills++;
            scoreInt += 100;

            if (Random.Range(0, 100) < 15)
            {
                SpawnRune();
            }

            if (Random.Range(0, 100) < 10)
            {
                SpawnPowerup();
            }


            if (scoreInt > nextStageAt && !bossBattle)
            {
                bossBattle = true;
                StartCoroutine(IEBossBattle());
                return;
            }

            if (!isSpawning && activeEnemies.Count < maxEnemies && !bossBattle)
            {
                StartCoroutine(SpawnEnemies());
            }
        }

        if(enemy != null)
            StartCoroutine(DeadCoroutine(enemy));     
    }

    private void SpawnRune()
    {
        int r = Random.Range(0, runes.Length);
        Vector3 runeSpawnPos = new Vector3(Random.Range(minX, maxX), yval, 0f);
        Instantiate(runes[r], runeSpawnPos, Quaternion.identity);
    }

    private void SpawnPowerup()
    {
        Vector3 runeSpawnPos = new Vector3(Random.Range(minX, maxX), yval, 0f);
        Instantiate(powerup, runeSpawnPos, Quaternion.identity);
    }


    private IEnumerator IEBossBattle()
    {
        Vector3 spawnPos = new Vector3(14f, yval+25, 0f);

        GameObject b = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        b.GetComponent<Enemy>().player = playerObject;
        yield return new WaitForSeconds(3);
        ParticleSystem p = GameObject.FindGameObjectWithTag("bossParticleSpawn").GetComponent<ParticleSystem>();
        p.Play();
        CinemachineShake.Instance.ShakeCamera(10f, .3f);

        
        activeEnemies.Add(b);

        yield return null;
    }



    private IEnumerator DeadCoroutine(GameObject enemy)
    {
        if (enemy != null)
        {
            SpriteRenderer rend = enemy.GetComponent<SpriteRenderer>();

            ParticleSystem ps = enemy.transform.Find("blood").GetComponent<ParticleSystem>();
            ps.transform.parent = null;
            ps.Play();
            Destroy(ps, 2f);

            if (rend != null)
            {
                float duration = 1f;
                float t = 0f;
                while (t < 0.3f)
                {
                    t += Time.deltaTime / duration;
                    rend.material.SetFloat("_DissolveThreshold", t);
                    yield return null;
                }
                rend.material.SetFloat("_DissolveThreshold", 1.0f);
            }

            Destroy(enemy.gameObject);
        }
    }
}