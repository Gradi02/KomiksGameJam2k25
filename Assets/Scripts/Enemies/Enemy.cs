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


}
