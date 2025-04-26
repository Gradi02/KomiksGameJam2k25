using UnityEngine;

public class PewPewBomb : MonoBehaviour
{
    public GameObject bombPrefab;   // Prefab bomby
    public Transform shootPoint;
    [SerializeField] private float bombForce = 5f;
    [SerializeField] private float fireRate = 1.5f;  // Trochê wolniejsze od strzelania
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private GameObject explosionEffectPrefab; // Efekt eksplozji (particles)

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse1) && fireTimer >= fireRate) // Prawy przycisk myszy
        {
            ShootBomb();
            fireTimer = 0f;
        }
    }

    void ShootBomb()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mouseWorldPos - shootPoint.position);
        shootDirection.Normalize();

        GameObject bomb = Instantiate(bombPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bombForce;
        }

        // Skrypt bomby ¿eby wybuch³a
        bomb.AddComponent<BombExplode>().Setup(explosionRadius, explosionForce, explosionEffectPrefab);

        Destroy(bomb, 5f); // Jeœli nie trafi w nic, usuwamy po 5s
    }
}