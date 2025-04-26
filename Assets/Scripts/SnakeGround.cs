using Unity.VisualScripting;
using UnityEngine;

public class SnakeGround : MonoBehaviour
{
    public float speed = 10f;  // Szybkość poruszania się obiektu
    public float max_distance = 30;
    public float how_much_distance = 0;
    public Vector3 direction;

    private Animator animator;

    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Initialize(new Vector3(1,0,0));
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {

        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("snakegroundmove"));
        // Debug.Log(how_much_distance);
        // Poruszamy obiekt w kierunku znormalizowanego wektora
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("snakegroundattack")){
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
                Destroy(this);
            }
        } else {
            if(how_much_distance >= max_distance){
                animator.SetBool("attack",true);
            } else {
                Debug.Log(direction);
                transform.position = new Vector3((direction * speed * Time.deltaTime).x, transform.position.y, transform.position.z);
                how_much_distance+=Time.deltaTime * speed;
            }    
        }    
    }

    // Sprawdzanie kolizji z innymi obiektami
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("KOLIZJA");
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {
            speed = 1;
            animator.SetBool("attack",true);
            Debug.Log("Trafiono w wroga!");
        }
    }
}
