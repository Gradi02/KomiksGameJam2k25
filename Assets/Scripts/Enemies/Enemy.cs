using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health { get; protected set; } = 100;
    protected float SqrDstToPlayer = 0;
    [SerializeField] bool isBoss = false;

    [Header("States")]
    protected State[] states;


    [Header("References")]
    protected StateMachine machine;
    [SerializeField] public GameObject player;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Material mat;

    [Header("Shader flash")]
    public string shaderProperty = "_power"; // nazwa parametru w shaderze
    public float flashDuration = 0.5f; // czas trwania flasha
    public float maxFlashValue = 0.4f;


    public void Spawn(GameObject pl)
    {
        player = pl;
    }


    public void TakeDamage(int val)
    {
        AudioManager.instance.Play("enemyDeath");
        health -= val;
        if (health <= 0)
        {
            DestroyEnemy();
        }
        else
        {
            FlashCoroutine();
        }
    }


    public void DestroyEnemy()
    {
        spriteRenderer.material = mat;
        SpawnerManager.Instance.OnEnemyKilled(this.gameObject, isBoss);
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

    private IEnumerator FlashCoroutine()
    {
        float timer = 0f;

        // Najpierw szybko w górê
        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(0f, maxFlashValue, timer / (flashDuration / 2));
            spriteRenderer.material.SetFloat(shaderProperty, value);
            yield return null;
        }

        timer = 0f;

        // Potem p³ynnie w dó³
        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(maxFlashValue, 0f, timer / (flashDuration / 2));
            spriteRenderer.material.SetFloat(shaderProperty, value);
            yield return null;
        }

        spriteRenderer.material.SetFloat(shaderProperty, 0f); // upewnij siê, ¿e na koñcu jest 0
    }
}
