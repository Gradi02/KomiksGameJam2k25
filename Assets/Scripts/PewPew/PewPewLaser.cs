using UnityEngine;

public class PewPewLaser : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private LayerMask hitLayers; // Na kt�re warstwy laser reaguje
    [SerializeField] private LineRenderer lineRendererPrefab; // Prefab lasera
    [SerializeField] private float laserDuration = 0.05f; // Jak d�ugo laser jest widoczny

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

        // Raycast przeszywaj�cy � trafia WSZYSTKIE obiekty na drodze
        RaycastHit2D[] hits = Physics2D.RaycastAll(shootPoint.position, shootDirection, maxDistance, hitLayers);

        // Pokazujemy lini� lasera
        if (currentLaser != null)
        {
            Destroy(currentLaser.gameObject);
        }

        currentLaser = Instantiate(lineRendererPrefab);
        currentLaser.SetPosition(0, shootPoint.position);

        if (hits.Length > 0)
        {
            // Je�li co� trafili�my � laser ko�czy si� na najdalszym trafieniu
            Vector2 farthestHit = shootPoint.position;
            foreach (var hit in hits)
            {
                farthestHit = hit.point;
                Instantiate(particleHitPrefab, hit.point, Quaternion.identity);
                // Mo�esz co� zrobi� z obiektami np. je niszczy�:
                // Destroy(hit.collider.gameObject);
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            currentLaser.SetPosition(1, farthestHit);
        }
        else
        {
            // Je�li nic nie trafili�my � laser idzie w dal
            currentLaser.SetPosition(1, (Vector2)shootPoint.position + shootDirection * maxDistance);
        }

        // Usuwamy laser po czasie
        Destroy(currentLaser.gameObject, laserDuration);
    }
}
