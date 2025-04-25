using Unity.VisualScripting;
using UnityEngine;

public class SigilInteraction : MonoBehaviour
{
    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag == "EnvSign" && Input.GetButtonDown("Interact")){
            collision.gameObject.GetComponent<EnvSignInteractionAction>().Play();
        }
    }
}
