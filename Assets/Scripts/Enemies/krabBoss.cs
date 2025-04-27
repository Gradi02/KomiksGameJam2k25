using UnityEngine;

public class krabBoss : Enemy
{
    [SerializeField] private float dstToAttack = 10;

    [Header("States")]
    [SerializeField] private krabScreamState scream, scream2;
    [SerializeField] private krabShotState shot;
    [SerializeField] private walkState walk;
    [SerializeField] private krabRunState run;

    public int phase { get; private set; } = 1;

    public void Start()
    {
        machine = GetComponent<StateMachine>();
        states = GetComponentsInChildren<State>();
        health = 1000;

        foreach (var state in states)
        {
            state.InitState(animator, rb, player, machine);
        }
    }

    private void FixedUpdate()
    {
        SqrDstToPlayer = Vector2.Distance(transform.position, player.transform.position);
        UpdatePhase();
        SelectState();
    }

    private void UpdatePhase()
    {
        if (health > 800 && health <= 1000)
        {
            phase = 1;
        }
        else if (health > 500 && health <= 800)
        {
            phase = 2;
        }
        else
        {
            phase = 3;
        }
    }

    private void SelectState()
    {
        switch (phase)
        {
            case 1:
                if (SqrDstToPlayer <= dstToAttack)
                {
                    machine.RequestChangeState(shot);
                }
                else
                {
                    machine.RequestChangeState(walk);
                }
                break;

            case 2:
                if (SqrDstToPlayer <= dstToAttack)
                {
                    machine.RequestChangeState(scream);
                }
                else
                {
                    machine.RequestChangeState(walk);
                }
                break;

            case 3:
                if (SqrDstToPlayer <= dstToAttack)
                {
                    machine.RequestChangeState(scream2);
                }
                else
                {
                    machine.RequestChangeState(run);
                }
                break;
        }
    }
}
