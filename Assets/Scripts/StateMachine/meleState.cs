using UnityEngine;

public class meleState : State
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private float delayBetweenHits = 1.5f;
    private float nextHit = 0;
    private float dstToHit = 2f;

    public override void StartState()
    {
        base.StartState();
        isEnded = true;
        nextHit = Time.time + delayBetweenHits;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector2 playerPos = player.transform.position;
        float dst = Vector2.Distance(playerPos, transform.position);

        if (dst > dstToHit)
        {
            Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 dir = (playerPos - enemyPos).normalized;

            rb.linearVelocity = new Vector2(speed * dir.x, rb.linearVelocity.y);
        }

        if ((playerPos - new Vector2(transform.position.x, transform.position.y)).normalized.x > 0.01f)
            render.flipX = false;
        else
            render.flipX = true;

        if(Time.time > nextHit)
        {
            nextHit = Time.time + delayBetweenHits;
            if(Vector2.Distance(playerPos, transform.position) < dstToHit)
            {
                player.GetComponent<HealthManager>().TakeDamage(5);
            }
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
