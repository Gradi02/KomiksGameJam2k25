using UnityEngine;

public class walkState : State
{
    [SerializeField] private float speed;


    public override void StartState()
    {
        base.StartState();
        isEnded = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (playerPos - enemyPos).normalized;

        rb.linearVelocity = new Vector2(speed * dir.x, rb.linearVelocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
