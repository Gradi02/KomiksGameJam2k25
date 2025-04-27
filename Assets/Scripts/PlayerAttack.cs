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
                    // Vector2 snakeDirection = player.transform.right;

                    AudioManager.instance.Play("snake");
                    Vector3 snakeGroundPosition = player.transform.position;
                    snakeGroundPosition.y -= 0.8f;
                    Instantiate(snakeGroundPrefab, snakeGroundPosition, Quaternion.Euler(0, 0, 0));
                    //snakeGround.GetComponent<SnakeGround>().Initialize(snakeDirection);

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
