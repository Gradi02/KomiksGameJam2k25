using System.Collections;
using UnityEngine;

public class InterStartGame : EnvSignInteractionAction
{
    public GameObject firstSigil;
    public Transform SpawnPlace;
    public GameObject wholeMenu;
    public GameObject infoFrame;

    public GameObject Sign;

    public GameObject[] menuObjects;
    private float[] menuDelayHiders = { 0, 0.3f, 0.6f };

    private IEnumerator StartGame()
    {
        AudioManager.instance.Stop("welcomeMusic");
        LeanTween.scale(infoFrame, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInOutCubic);
        for (int i = 0; i < menuObjects.Length; i++)
        {
            LeanTween.scale(menuObjects[i], Vector3.zero, 0.4f).setDelay(menuDelayHiders[i]).setEase(LeanTweenType.easeInOutCubic);
        }
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Stop("gameWave_1");
        LeanTween.rotateZ(Sign, 0f, 1f).setEase(LeanTweenType.easeInOutBounce);
        yield return new WaitForSeconds(0.5f);
        infoFrame.SetActive(false);
        wholeMenu.SetActive(false);
        
    }
    public override void Play(EqManager eq)
    {
        Debug.Log("start");
        Instantiate(firstSigil, SpawnPlace);
        StartCoroutine(StartGame());

    }
}
