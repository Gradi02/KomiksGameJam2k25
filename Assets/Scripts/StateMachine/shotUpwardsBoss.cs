using System.Collections;
using UnityEngine;

public class shotUpwardsBoss : State
{
    [SerializeField] private float delayToShot = 0.3f;
    [SerializeField] private float timeToEnd = 1.5f;
    [SerializeField] private float arrowFallDelay = 0.5f;
    [SerializeField] private int fallingArrowCount = 5;
    [SerializeField] private float fallingArrowSpread = 2f;
    [SerializeField] private float shotCooldown = 2f;
    private float t1, t2, lastShotTime;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSp;
    private bool shoted;

    public override void StartState()
    {
        Debug.Log("shotupwards");
        base.StartState();
        t1 = Time.time + delayToShot;
        t2 = Time.time + timeToEnd;
        lastShotTime = -shotCooldown;
        shoted = false;
        rb.linearVelocity = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Time.time > t1 && !shoted && Time.time >= lastShotTime + shotCooldown)
        {
            shoted = true;
            lastShotTime = Time.time;
            Shot();
        }

        if (Time.time > t2)
        {
            isEnded = true;
            machine.RequestExitState();
        }
    }

    public override void EndState()
    {
        base.EndState();
        AudioManager.instance.Play("boom");
    }

    public void Shot()
    {
        AudioManager.instance.Play("arrow");
        // Instantiate the arrow at the specified position and rotation  
        GameObject a = Instantiate(arrow, arrowSp.transform.position, transform.rotation);
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();

        // Set the arrow's velocity to go upwards towards the sky  
        rb.linearVelocity = new Vector2(0, 28);
        rb.gravityScale = 0; // Disable gravity for the arrow initially

        // Apply gravity to the arrow after a delay  
        //StartCoroutine(ApplyGravityAfterDelay(rb, 0.5f));

        // Spawn falling arrows after the upward arrow  
        StartCoroutine(SpawnFallingArrows());
    }

    private IEnumerator ApplyGravityAfterDelay(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.gravityScale = 1;
    }

    private IEnumerator SpawnFallingArrows()
    {
        yield return new WaitForSeconds(arrowFallDelay);

        for (int i = 0; i < fallingArrowCount; i++)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(Random.Range(-fallingArrowSpread, fallingArrowSpread), Random.Range(13f, 16f), 0);
            GameObject fallingArrow = Instantiate(arrow, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = fallingArrow.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
        }
        AudioManager.instance.Play("boom");
    }
}
