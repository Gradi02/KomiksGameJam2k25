using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] public GameObject waveManager;
    public bool startGame = false;
    public string currentBackgroundMusic;

    void Start(){
        AudioManager.instance.PlayLoop("menu");
        currentBackgroundMusic = "menu";
    }

    public void StartGame(){
        startGame = true;
    }

    public void ResetGame(){
        startGame = false;
        AudioManager.instance.Stop(currentBackgroundMusic);
        AudioManager.instance.PlayLoop("menu");
        currentBackgroundMusic = "menu";
    }

    void Update(){
        if(startGame)
        {
            var scorei = waveManager.GetComponent<SpawnerManager>().scoreInt;
            Debug.Log(scorei);
            if(scorei <=500 && currentBackgroundMusic != "gameWave_1"){
                Debug.Log("FIRST AMBIENT");
                AudioManager.instance.Stop(currentBackgroundMusic);
                currentBackgroundMusic = "gameWave_1";
                AudioManager.instance.PlayLoop("gameWave_1");
            }else if(scorei <= 1500 && scorei >500 && currentBackgroundMusic != "gameWave_2"){
                AudioManager.instance.Stop(currentBackgroundMusic);
                currentBackgroundMusic = "gameWave_2";
                AudioManager.instance.PlayLoop("gameWave_2");
            }else if(scorei < 2000 && scorei >1500 && currentBackgroundMusic != "gameWave_3"){
                AudioManager.instance.Stop(currentBackgroundMusic);
                currentBackgroundMusic = "gameWave_3";
                AudioManager.instance.PlayLoop("gameWave_3");
            }else if(scorei >= 2000 && currentBackgroundMusic != "gameWave_4"){
                AudioManager.instance.Stop(currentBackgroundMusic);
                currentBackgroundMusic = "gameWave_4";
                AudioManager.instance.PlayLoop("gameWave_4");
            }
        }
    }
}
