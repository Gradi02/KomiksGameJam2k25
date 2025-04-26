using UnityEngine;

public class PewPewGravity : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    [SerializeField] private float bulletForce = 5f;
    [SerializeField] private float fireRate = 0.1f;

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mouseWorldPos - shootPoint.position);
        shootDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletForce;
        }

        Destroy(bullet, 5f);
    }
}