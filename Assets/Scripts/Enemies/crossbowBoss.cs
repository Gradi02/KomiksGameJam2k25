using UnityEngine;

public class crossbowBoss : Enemy
{
    [SerializeField] private float dstToAttack = 10;

    [Header("States")]
    [SerializeField] private idleBoss idleBoss;
    [SerializeField] private quickShotBoss quickShotBoss;
    [SerializeField] private shotBoss shotBoss;
    [SerializeField] private runBoss runBoss;


    public void Start()
    {
        machine = GetComponent<StateMachine>();
        states = GetComponentsInChildren<State>();

        foreach (var state in states)
        {
            state.InitState(animator, rb, player, machine);
        }
    }

    private void FixedUpdate()
    {
        SqrDstToPlayer = Vector2.Distance(transform.position, player.transform.position);
        SelectState();
    }

    private void SelectState()
    {
        if (SqrDstToPlayer <= dstToAttack)
        {
            machine.RequestChangeState(shotBoss);
        }
        else
        {
            machine.RequestChangeState(runBoss);
        }
    }
}
