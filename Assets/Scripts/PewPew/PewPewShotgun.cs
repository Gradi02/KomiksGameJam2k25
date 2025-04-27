using UnityEngine;

public class PewPewShotgun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    [SerializeField] private int bulletsPerShot = 8;
    [SerializeField] private float spreadAngle = 40f;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireRate = 0.5f;

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && fireTimer >= fireRate) // 
        {
            ShootShotgun();
            fireTimer = 0f;
        }
    }

    void ShootShotgun()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector2 baseDirection = (mouseWorldPos - shootPoint.position).normalized;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float angleOffset = Random.Range(-spreadAngle, spreadAngle);
            Vector2 shootDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletForce;
            }

            Destroy(bullet, 3f);
        }
    }
}