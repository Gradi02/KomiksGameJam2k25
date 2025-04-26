using UnityEngine;

public class InterHelp : EnvSignInteractionAction
{
    public GameObject infoCanva;
    public override void Play(EqManager eq)
    {
        if (infoCanva.activeSelf)
        {
            // Jeœli jest aktywne — schowaj z animacj¹
            LeanTween.scale(infoCanva, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                infoCanva.SetActive(false);
            });
        }
        else
        {
            // Jeœli nieaktywne — poka¿ z animacj¹
            infoCanva.SetActive(true);
            infoCanva.transform.localScale = Vector3.zero;
            LeanTween.scale(infoCanva, Vector3.one, 1f).setEase(LeanTweenType.easeInOutBounce);
        }
    }
}
