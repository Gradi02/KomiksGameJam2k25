using UnityEngine;

public class arrowMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 2f;
    private Transform player;
    private Vector2 moveDirection;
    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (player.position - transform.position).normalized;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 targetDirection = (player.position - transform.position).normalized;
            moveDirection = Vector2.Lerp(moveDirection, targetDirection, rotateSpeed * Time.deltaTime);
        }

        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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

    public void InvokeDestroy(float s)
    {
        Invoke("DestroyArrow", s);
    }
}
