using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreStartGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup title;
    [SerializeField] private CanvasGroup lore;

    void Start()
    {
        title.alpha = 0;
        lore.alpha = 0;
        StartCoroutine(LoreStart());
    }

    IEnumerator LoreStart()
    {
        // Fade in Title
        title.gameObject.SetActive(true);
        LeanTween.alphaCanvas(title, 1f, 2f);
        yield return new WaitForSeconds(3f);

        // Fade out Title
        LeanTween.alphaCanvas(title, 0f, 1f);
        yield return new WaitForSeconds(2f);
        title.gameObject.SetActive(false);

        // Fade in Lore
        lore.gameObject.SetActive(true);
        LeanTween.alphaCanvas(lore, 1f, 5f);
        yield return new WaitForSeconds(8f);

        // Fade out Title
        LeanTween.alphaCanvas(lore, 0f, 1f);
        yield return new WaitForSeconds(2f);
        title.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }
}