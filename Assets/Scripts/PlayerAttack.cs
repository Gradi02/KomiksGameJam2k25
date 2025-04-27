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
        //ATAK MEGAWENSZA9
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var player = GameObject.FindWithTag("Player");

                if(player.GetComponent<PlayerMovement>().IsGrounded()){
                    AudioManager.instance.Play("hiss");
                    // Vector2 snakeDirection = player.transform.right;
                    Vector3 snakeGroundPosition = player.transform.position;
                    snakeGroundPosition.y -= 1;
                    Instantiate(snakeGroundPrefab, snakeGroundPosition, Quaternion.Euler(0, 0, 0));
                    //snakeGround.GetComponent<SnakeGround>().Initialize(snakeDirection);

                    timeBtwAttack = startTimeBtwAttack;

                } else {
                    AudioManager.instance.Play("shoot");
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
