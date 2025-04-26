using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuIcons;
    private float[] delays = { 0.2f, 0.4f, 0.6f };
    void Start()
    {
        for (int i = 0; i < menuIcons.Length; i++)
        {
            LeanTween.moveLocalY(menuIcons[i].gameObject, +0.3f, 2f).setDelay(delays[i]).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
        }
    }

    
}
