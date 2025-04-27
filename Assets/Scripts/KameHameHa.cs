using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KameHameHa : MonoBehaviour
{
    public Light2D GlobalLight;
    private int damage = 300;
    public IEnumerator KameHameHaCoroutine()
    {

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<HealthManager>().invincible = true;
        //znajdz wszystkie obiekty w layer bullet i destroy
        DestroyAllBullets();

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

        List<GameObject> enemiesToDmg = new List<GameObject>(SpawnerManager.Instance.activeEnemies);

        foreach (GameObject enemy in enemiesToDmg)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        DestroyAllBullets();



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

        yield return new WaitForSeconds(1.1f);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<HealthManager>().invincible = false;
        yield return new WaitForSeconds(1.1f);
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(KameHameHaCoroutine());
        }
    }
    */


    void DestroyAllBullets()
    {
        // Znajdü wszystkie GameObjecty w scenie
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Sprawdü czy obiekt jest na warstwie "Bullet"
            if (obj.layer == LayerMask.NameToLayer("bullet"))
            {
                Destroy(obj);
            }
        }
    }
}
