using UnityEngine;

public class StandardEnemy : Enemy
{
    [SerializeField] private float dstToAttack = 10;

    [Header("States")]
    [SerializeField] private walkState walkS;
    [SerializeField] private attackState attackS;


    public void Start()
    {
        machine = GetComponent<StateMachine>();
        states = GetComponentsInChildren<State>();
        
        foreach(var state in states)
        {
            state.InitState(animator, rb, player);
        }
    }

    private void FixedUpdate()
    {
        SqrDstToPlayer = Vector2.Distance(transform.position, player.transform.position);
        SelectState();
    }

    private void SelectState()
    {
        Debug.Log(SqrDstToPlayer);
        if(SqrDstToPlayer <= dstToAttack)
        {
            machine.RequestChangeState(attackS);
        }
        else
        {
            machine.RequestChangeState(walkS);
        }
    }
}
