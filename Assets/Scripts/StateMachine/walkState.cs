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

    }

    public override void EndState()
    {
        base.EndState();
    }
}
