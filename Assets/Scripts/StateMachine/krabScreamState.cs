using System.Collections;
using UnityEngine;

public class krabScreamState : State
{
    [SerializeField] private float screamDuration = 3f; // czas trwania krzyku
    [SerializeField] private float spawnInterval = 0.3f; // co ile sekund spada pocisk
    [SerializeField] private GameObject fallingProjectile; // prefab pocisku
    [SerializeField] private Vector2 spawnAreaMin; // lewy dolny róg strefy spawn
    [SerializeField] private Vector2 spawnAreaMax; // prawy górny róg strefy spawn

    private float endTime;
    private Coroutine spawnRoutine;

    public override void StartState()
    {
        base.StartState();
        Debug.Log("Scream Start!");

        rb.linearVelocity = Vector2.zero; // zatrzymujemy ruch
        // dŸwiêk krzyku
        endTime = Time.time + screamDuration;

        spawnRoutine = StartCoroutine(SpawnProjectiles());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Time.time > endTime)
        {
            isEnded = true;
            machine.RequestExitState();
        }
    }

    public override void EndState()
    {
        base.EndState();

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }

    private IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            AudioManager.instance.Play("shot");
            SpawnProjectile();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnProjectile()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        Vector2 spawnPosition = new Vector2(randomX, spawnAreaMax.y);
        GameObject proj = Instantiate(fallingProjectile, spawnPosition, Quaternion.identity);

        Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D>();
        if (rbProj != null)
        {
            rbProj.linearVelocity = Vector2.down * 5f; // spadaj¹ w dó³ z prêdkoœci¹
        }
    }
}
