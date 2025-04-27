using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] public GameObject waveManager;
    bool startGame = false;

    void Start(){
        PlayLoop("menu");
    }

    void StartGame(){
        startGame = true;
    }

    void PlayLoop(string clipName){

        AudioSource audioS = GetComponent<AudioSource>();
        audioS.clip = Resources.Load<AudioClip>("Audio/" + clipName);
        audioS.loop = true;
        audioS.Play();
    }

    void Update(){
        if(startGame)
        {
            var me = waveManager.GetComponent<SpawnerManager>().maxEnemies;
            if(me <=3){
                AudioManager.instance.PlayLoop("game_ambient_one");
            }else if(me <=5){
                AudioManager.instance.PlayLoop("game_ambient_two");
            }else if(me <=10){
                AudioManager.instance.PlayLoop("game_ambient_three");
            }
        }
    }
}
