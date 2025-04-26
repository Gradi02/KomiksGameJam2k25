using UnityEngine;

public class BombExplode : MonoBehaviour
{
    private float explosionRadius;
    private float explosionForce;
    private GameObject explosionEffectPrefab;

    public void Setup(float radius, float force, GameObject effectPrefab)
    {
        explosionRadius = radius;
        explosionForce = force;
        explosionEffectPrefab = effectPrefab;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Szukamy wszystkich obiektów w promieniu
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                float distance = Mathf.Max(0.1f, direction.magnitude);
                rb.AddForce(direction.normalized * (explosionForce / distance), ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject); // Usuwamy bombê
    }

    void OnDrawGizmosSelected()
    {
        // Tylko dla podgl¹du w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}