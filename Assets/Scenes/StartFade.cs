using System.Collections;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CanvasGroup bg;
    void Start()
    {
        bg.gameObject.SetActive(true);
        StartCoroutine(StartFadeC());
    }

    IEnumerator StartFadeC()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.alphaCanvas(bg, 0f, 1f);
        yield return new WaitForSeconds(1.2f);
        bg.gameObject.SetActive(false);
    }
}
