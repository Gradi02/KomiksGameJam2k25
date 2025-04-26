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

    private void ShootUpwards()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (playerPos - enemyPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //GameObject a = Instantiate(shotS.arrow, shotS.arrowSp.transform.position, rotation);
        //a.GetComponent<arrowMovement>().InvokeDestroy(4);
    }
}
