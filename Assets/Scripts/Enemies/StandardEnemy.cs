using UnityEngine;

public class StandardEnemy : Enemy
{
    [SerializeField] private float dstToAttack = 10;

    [Header("States")]
    [SerializeField] private walkState walkS;
    [SerializeField] private attackState attackS;
    [SerializeField] private jumpState jumpS;


    public void Start()
    {
        machine = GetComponent<StateMachine>();
        states = GetComponentsInChildren<State>();
        
        foreach(var state in states)
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
        bool isGrounded = IsGrounded();
        bool isPlayerAbove = player.transform.position.y > transform.position.y + 0.5f;
        bool seePlayer = SeePlayer();

        if (!seePlayer && isPlayerAbove && isGrounded)
        {
            machine.RequestChangeState(jumpS);
        }
        else if (SqrDstToPlayer <= dstToAttack)
        {
            machine.RequestChangeState(attackS);
        }
        else
        {
            machine.RequestChangeState(walkS);
        }
    }
}
