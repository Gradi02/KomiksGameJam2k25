using UnityEngine;

public class attackState : State
{
    [SerializeField] private float attackTime = 2;
    private float endIn = 0;



    public override void StartState()
    {
        base.StartState();
        endIn = Time.time + attackTime;
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
