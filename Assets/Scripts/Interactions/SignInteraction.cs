using Unity.VisualScripting;
using UnityEngine;

public class SigilInteraction : MonoBehaviour
{
    private float playerInteractionRange = 2;
    private float nextInteract = 0, interactDelay = 0.1f;
    [SerializeField] private LayerMask interactionmask;

    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag == "EnvSign" && Input.GetButtonDown("Interact")){
            collision.gameObject.GetComponent<EnvSignInteractionAction>().Play();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && Time.time > nextInteract)
        {
            Collider2D nearest = FindNearestInteractive();
            if (nearest != null)
            {
                nearest.GetComponent<EnvSignInteractionAction>().Play();
            }
            nextInteract = Time.time + interactDelay;
        }
    }

    Collider2D FindNearestInteractive()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerInteractionRange, interactionmask);
        Collider2D nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider2D col in colliders)
        {
            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = col;
            }
        }

        return nearest;
    }
}
