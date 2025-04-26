using UnityEngine;

public class PewPewGravity : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.05f;
    public float bulletForce = 500f;
    public float spreadAngle = 5f;
    public float maxDistance = 1000f; // Jak daleko mo¿e lecieæ promieñ

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            targetPoint = hit.point; // Trafiliœmy coœ
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxDistance; // Nie trafiliœmy, strzelamy w dal
        }

        Vector3 direction = (targetPoint - shootPoint.position).normalized;

        // Dodajemy lekki rozrzut
        direction = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0) * direction;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.AddForce(direction * bulletForce);
        }

        Destroy(bullet, 5f);
    }
}