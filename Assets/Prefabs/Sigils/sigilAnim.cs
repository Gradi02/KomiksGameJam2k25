using UnityEngine;

public class SigilAnim : MonoBehaviour
{
    void Start()
    {
        LeanTween.moveLocalY(this.gameObject, +0.5f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }
}
