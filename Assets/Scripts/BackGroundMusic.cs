using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] public GameObject waveManager;

    void Start(){
        AudioManager.instance.PlayLoop("menu");
    }

    void Update(){

    }
}
