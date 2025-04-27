using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KameHameHa : MonoBehaviour
{
    public Light2D GlobalLight;
    public IEnumerator KameHameHaCoroutine()
    {
        LeanTween.value(1, 15, 4f)
            .setOnUpdate((float val) => {
                GlobalLight.intensity = val;
            })
            .setEase(LeanTweenType.easeInOutSine); // 1111111111111111111

        LeanTween.value(1, 15, 5f)
            .setOnUpdate((float val) => {
                CinemachineShake.Instance.ShakeCamera(val, .1f);
            })
            .setEase(LeanTweenType.easeInOutSine); //22222222222222222222
        
        yield return new WaitForSeconds(4.1f);

        //TUTAJ ROZPIERDOL WSZYSTKICH PRZECIWNIKÓW
        // I WYLACZ SKRYPTY MOVEMENT I COLLISION NA GRACZU




            LeanTween.value(15, 5000, 1f)
        .setOnUpdate((float val) => {
            GlobalLight.intensity = val;
        })
        .setEase(LeanTweenType.easeInOutSine); // 1111111111111111111
        yield return new WaitForSeconds(1.1f);
            LeanTween.value(5000, 1, 1f)
       .setOnUpdate((float val) => {
           GlobalLight.intensity = val;
       })
       .setEase(LeanTweenType.easeInOutSine); // 1111111111111111111

        LeanTween.value(15, 1, 2.1f)
            .setOnUpdate((float val) => {
                CinemachineShake.Instance.ShakeCamera(val, .1f);
            })
            .setEase(LeanTweenType.easeInOutSine); //22222222222222222222
        yield return new WaitForSeconds(2.2f);
    }
}
