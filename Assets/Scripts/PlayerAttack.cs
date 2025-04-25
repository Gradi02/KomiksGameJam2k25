using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] slotsBg;
    private float timeBtwAttack = 0.1f;
    [SerializeField] float startTimeBtwAttack;

    public GameObject snakePrefab;
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
    }

    void Update()
    {
        Debug.Log(timeBtwAttack);
        //ATAK MEGAWENSZA9
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - firePoint.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

                GameObject snake = Instantiate(snakePrefab, firePoint.position, rotation);
                snake.GetComponent<Rigidbody2D>().linearVelocity = direction * snakeSpeed;
                timeBtwAttack = startTimeBtwAttack;
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
