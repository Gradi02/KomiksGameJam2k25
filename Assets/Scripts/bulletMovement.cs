using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float speed = 5f;     
    private Vector2 moveDirection;
    private Transform player;
    [SerializeField] private ParticleSystem ps;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Trafiony");
            player.GetComponent<HealthManager>().TakeDamage(5);
            DestroyArrow();
        }
        else
        {
            DestroyArrow();
        }
    }

    public void DestroyArrow()
    {
        ps.transform.parent = null;
        ps.Play();
        Destroy(ps.gameObject, 2);
        Destroy(gameObject);
    }
}
