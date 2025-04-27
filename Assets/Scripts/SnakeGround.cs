using Unity.VisualScripting;
using UnityEngine;

public class SnakeGround : MonoBehaviour
{
    public float speed = 10f;  // Szybkość poruszania się obiektu
    [SerializeField] private float max_distance = 10.0f;
    private int damage = 100;
    public float how_much_distance = 0;
    public int direction;
    public GameObject whoGetHit;
    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > player.transform.position.x){
            direction = 1;
        } else {
            direction = -1;
        }

    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("snakegroundattack"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (whoGetHit != null)
                {
                    whoGetHit.GetComponent<Enemy>().TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (how_much_distance >= max_distance)
            {
                Destroy(gameObject); // <<-- Teraz znika normalnie, BEZ animacji ataku!
            }
            else
            {
                transform.position = new Vector3(
                    direction * speed * Time.deltaTime + transform.position.x,
                    transform.position.y,
                    transform.position.z
                );

                how_much_distance += Time.deltaTime * speed;
            }
        }
    }

    // Sprawdzanie kolizji z innymi obiektami

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.NameToLayer("Enemy") == collision.gameObject.layer)
        {
            transform.position = new Vector3(collision.gameObject.transform.position.x, transform.position.y + 0.5f, transform.position.z);
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            speed = 1;
            animator.speed = 2;
            animator.SetBool("attack",true);

            whoGetHit = collision.gameObject;
        }
    }
}
