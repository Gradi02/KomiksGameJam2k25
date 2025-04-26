using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] slotsBg;
    public GameObject[] LoadSlots;
    private float timeBtwAttack = 0.1f;
    private float timeBtwLoad = 2.0f;
    private int maxLoad = 3;
    private int load = 0;

    [SerializeField] float startTimeBtwAttack;
    [SerializeField] float startTimeBtwLoad;


    public GameObject snakePrefab;
    public GameObject snakeGroundPrefab;

    public Transform firePoint;
    public float snakeSpeed = 10f;

    public int currentID = 0;
    private int maxID = 2;

    private void Start()
    {
        for (int i = 0; i < slotsBg.Length; i++)
        {
            slotsBg[i].SetActive(false);
        }
        slotsBg[currentID].SetActive(true);
        for (int i = 0; i < maxLoad; i++)
        {
            LoadSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    void Update()
    {
        //Debug.Log(timeBtwAttack);

        if (load < maxLoad)
        {
            if (timeBtwLoad <= 0)
            {
                load++;
                timeBtwLoad = startTimeBtwLoad;

                for (int i = 0; i < maxLoad; i++)
                {
                    LoadSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                }
                for (int i = 0; i < load; i++)
                {
                    LoadSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                }
            }
            else
            {
                timeBtwLoad -= Time.deltaTime;
            }
        }




        //ATAK MEGAWENSZA9
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && load>0)
            {
                for (int i = 0; i < maxLoad; i++)
                {
                    LoadSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                }
                load--;
                for (int i = 0; i < load; i++)
                {
                    LoadSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                }

                var player = GameObject.FindWithTag("Player");

                if(player.GetComponent<PlayerMovement>().IsGrounded()){
                    // Vector2 snakeDirection = player.transform.right;
                    Vector3 snakeGroundPosition = player.transform.position;
                    snakeGroundPosition.y -= 1;
                    GameObject snakeGround = Instantiate(snakeGroundPrefab, snakeGroundPosition, Quaternion.Euler(0, 0, 0));
                    // snakeGround.GetComponent<SnakeGround>().Initialize(snakeDirection);

                    timeBtwAttack = startTimeBtwAttack;
                    timeBtwLoad = startTimeBtwLoad;

                } else {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 direction = (mousePos - firePoint.position).normalized;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

                    GameObject snake = Instantiate(snakePrefab, firePoint.position, rotation);
                    snake.GetComponent<Rigidbody2D>().linearVelocity = direction * snakeSpeed;
                    timeBtwAttack = startTimeBtwAttack;
                    timeBtwLoad = startTimeBtwLoad;
                }

            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentID++;
            if (currentID > maxID)
            {
                currentID = 0;
            }

            for (int i = 0; i < slotsBg.Length; i++)
            {
                slotsBg[i].SetActive(false);
            }
            slotsBg[currentID].SetActive(true);

        }
        else if (scroll < 0f)
        {
            currentID--;
            if (currentID < 0)
            {
                currentID = maxID;
            }

            for (int i = 0; i < slotsBg.Length; i++)
            {
                slotsBg[i].SetActive(false);
            }
            slotsBg[currentID].SetActive(true);
        }
    }
}
