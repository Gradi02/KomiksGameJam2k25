using UnityEngine;

public class attackState : State
{
    [SerializeField] private float attackTime = 2;
    [SerializeField] private int damage = 10;
    private float endIn = 0;



    public override void StartState()
    {
        base.StartState();
        endIn = Time.time + attackTime;
        rb.linearVelocity = Vector2.zero;
        player.GetComponent<HealthManager>().TakeDamage(damage);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Time.time > endIn)
        {
            isEnded = true;
            machine.RequestExitState();
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
