using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack = 0.1f;

    [SerializeField] float startTimeBtwAttack;

    public GameObject snakePrefab;
    public GameObject snakeGroundPrefab;

    public Transform firePoint;
    public float snakeSpeed = 10f;

    void Update()
    {
        Debug.Log("oooooooooooooooooo");
        //ATAK MEGAWENSZA9
        if (timeBtwAttack <= 0)
        {
            Debug.Log("AAAAAAAAAAA");
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("BBBBBBBBBB");
                var player = GameObject.FindWithTag("Player");

                if(player.GetComponent<PlayerMovement>().IsGrounded()){
                    // Vector2 snakeDirection = player.transform.right;
                    Vector3 snakeGroundPosition = player.transform.position;
                    snakeGroundPosition.y -= 1;
                    Debug.Log("waaaz");
                    GameObject snakeGround = Instantiate(snakeGroundPrefab, snakeGroundPosition, Quaternion.Euler(0, 0, 0));
                    // snakeGround.GetComponent<SnakeGround>().Initialize(snakeDirection);

                    timeBtwAttack = startTimeBtwAttack;

                } else {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 direction = (mousePos - firePoint.position).normalized;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

                    GameObject snake = Instantiate(snakePrefab, firePoint.position, rotation);
                    snake.GetComponent<Rigidbody2D>().linearVelocity = direction * snakeSpeed;
                    timeBtwAttack = startTimeBtwAttack;
                }

            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
