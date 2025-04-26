using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] slotsBg;
    public GameObject[] LoadSlots;
    public GameObject snakePrefab;
    public Transform firePoint;
    public float snakeSpeed = 10f;

    [SerializeField] private float startTimeBtwAttack = 0.1f;

    private float timeBtwAttack;
    private int currentID = 0;
    private int maxID = 2;

    private void Start()
    {
        timeBtwAttack = startTimeBtwAttack;

        foreach (var slot in slotsBg)
            slot.SetActive(false);

        slotsBg[currentID].SetActive(true);
    }

    private void Update()
    {
        //HandleAttack();
        HandleScroll();
    }

/*    private void HandleAttack()
    {
        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0) && load > 0)
        {
            load--;
            UpdateLoadSlots();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject snake = Instantiate(snakePrefab, firePoint.position, Quaternion.Euler(0, 0, angle - 90));
            snake.GetComponent<Rigidbody2D>().linearVelocity = direction * snakeSpeed;

            timeBtwAttack = startTimeBtwAttack;
            timeBtwLoad = startTimeBtwLoad;
        }
    }*/

    private void HandleScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentID = (currentID + 1) % (maxID + 1);
            UpdateSlotsBg();
        }
        else if (scroll < 0f)
        {
            currentID = (currentID - 1 + (maxID + 1)) % (maxID + 1);
            UpdateSlotsBg();
        }
    }

    private void UpdateSlotsBg()
    {
        foreach (var slot in slotsBg)
            slot.SetActive(false);

        slotsBg[currentID].SetActive(true);
    }
}
