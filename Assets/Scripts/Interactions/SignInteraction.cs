using Unity.VisualScripting;
using UnityEngine;

public class SigilInteraction : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            EnvSignInteractionAction action = collision.GetComponent<EnvSignInteractionAction>();
            if(action != null)
            {
                action.Play(GetComponent<EqManager>());
            }
        }
    }


}
