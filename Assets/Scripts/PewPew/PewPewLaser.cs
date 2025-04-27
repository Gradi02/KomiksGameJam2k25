using UnityEngine;

public class PewPewLaser : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private LayerMask hitLayers; // Na które warstwy laser reaguje
    [SerializeField] private LineRenderer lineRendererPrefab; // Prefab lasera
    [SerializeField] private float laserDuration = 0.05f; // Jak d³ugo laser jest widoczny

    private LineRenderer currentLaser;
    public ParticleSystem particleHitPrefab;
    private int damage = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mouseWorldPos - shootPoint.position);
        shootDirection.Normalize();

        // Raycast przeszywaj¹cy — trafia WSZYSTKIE obiekty na drodze
        RaycastHit2D[] hits = Physics2D.RaycastAll(shootPoint.position, shootDirection, maxDistance, hitLayers);

        // Pokazujemy liniê lasera
        if (currentLaser != null)
        {
            Destroy(currentLaser.gameObject);
        }

        currentLaser = Instantiate(lineRendererPrefab);
        currentLaser.SetPosition(0, shootPoint.position);

        if (hits.Length > 0)
        {
            // Jeœli coœ trafiliœmy — laser koñczy siê na najdalszym trafieniu
            Vector2 farthestHit = shootPoint.position;
            foreach (var hit in hits)
            {
                farthestHit = hit.point;
                Instantiate(particleHitPrefab, hit.point, Quaternion.identity);
                // Mo¿esz coœ zrobiæ z obiektami np. je niszczyæ:
                // Destroy(hit.collider.gameObject);
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            currentLaser.SetPosition(1, farthestHit);
        }
        else
        {
            // Jeœli nic nie trafiliœmy — laser idzie w dal
            currentLaser.SetPosition(1, (Vector2)shootPoint.position + shootDirection * maxDistance);
        }

        // Usuwamy laser po czasie
        Destroy(currentLaser.gameObject, laserDuration);
    }
}
