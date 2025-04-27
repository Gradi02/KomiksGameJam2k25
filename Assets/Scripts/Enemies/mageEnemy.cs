using UnityEngine;

public class mageEnemy : Enemy
{
    [SerializeField] private float dstToAttack = 8;

    [Header("States")]
    [SerializeField] private walkState walkS;
    [SerializeField] private circleShotState attackS;


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
            machine.RequestChangeState(attackS);
        }
        else
        {
            machine.RequestChangeState(walkS);
        }
    }
}
