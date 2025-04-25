using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health { get; protected set; } = 100;
    protected float SqrDstToPlayer = 0;


    [Header("States")]
    protected State[] states;


    [Header("References")]
    protected StateMachine machine;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
     


    public void Spawn(GameObject pl)
    {
        player = pl;
    }


    public void TakeDamage(int val)
    {
        health -= val;
        if(health <= 0)
        {
            DestroyEnemy();
        }
    }


    public void DestroyEnemy()
    {


    }

    protected bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    protected bool SeePlayer()
    {
        Vector2 origin = transform.position;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, LayerMask.GetMask("Ground", "Player"));

        return hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }
}
