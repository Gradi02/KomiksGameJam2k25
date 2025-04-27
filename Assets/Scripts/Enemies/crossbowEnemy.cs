using UnityEngine;

public class crossbowEnemy : Enemy
{
    [SerializeField] private float dstToAttack = 10;

    [Header("States")]
    [SerializeField] private walkState walkS;
    [SerializeField] private shotState shotS;


    public void Start()
    {
        AudioManager.instance.Play("enemySpawn");
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
            machine.RequestChangeState(shotS);
        }
        else
        {
            machine.RequestChangeState(walkS);
        }
    }
}
