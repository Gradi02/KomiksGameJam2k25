using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void StartGame()
    {
        //wystartuj
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
