using UnityEngine;

public class meleEnemy : Enemy
{
    [Header("States")]
    [SerializeField] private meleState meleS;


    public void Start()
    {
        AudioManager.instance.Play("enemySpawn");
        machine = GetComponent<StateMachine>();
        states = GetComponentsInChildren<State>();

        foreach (var state in states)
        {
            state.InitState(animator, rb, player, machine);
        }

        machine.RequestChangeState(meleS);
    }

    private void FixedUpdate()
    {
        SqrDstToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }
}
