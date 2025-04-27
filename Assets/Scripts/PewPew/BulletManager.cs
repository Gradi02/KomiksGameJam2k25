using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damage = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("Enemy") == collision.gameObject.layer)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
