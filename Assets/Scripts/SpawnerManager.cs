using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    private GameObject playerObject;
    public List<GameObject> enemies;
    private int totalKills = 0;
    [SerializeField] private GameObject warningObj;

    [SerializeField] private float minX = -11f;
    [SerializeField] private float maxX = 40f;
    [SerializeField] private float yval = -2f;

    [SerializeField] private int maxEnemies = 5;

    public GameObject[] runes;
    private int spawned = 0;
    public List<GameObject> activeEnemies = new List<GameObject>();
    private bool isSpawning = false;
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
        playerObject = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        spawned = activeEnemies.Count;
    }
    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        while (activeEnemies.Count < maxEnemies)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), yval, 0f);

            GameObject warning = Instantiate(warningObj, spawnPos, Quaternion.identity);
            LeanTween.scale(warning, new Vector3(0.8f, 1f, 2f), 0.4f)
                .setEase(LeanTweenType.easeInOutSine)
                .setLoopPingPong();
            Destroy(warning, 2f);

            yield return new WaitForSeconds(2.1f);

            GameObject enemy = Instantiate(GetRandomEnemy(), spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().player = playerObject;
            activeEnemies.Add(enemy);
        }

        isSpawning = false; // KONIEC spawnienia dopóki nie zginie coœ
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

    public void OnEnemyKilled(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);

            totalKills++;

            if (totalKills % 10 == 0)
            {
                SpawnRune();
            }

            if (!isSpawning && activeEnemies.Count < maxEnemies)
            {
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    private void SpawnRune()
    {
        int r = Random.Range(0, runes.Length);
        Vector3 runeSpawnPos = new Vector3(Random.Range(minX, maxX), yval, 0f);
        Instantiate(runes[r], runeSpawnPos, Quaternion.identity);
    }
}