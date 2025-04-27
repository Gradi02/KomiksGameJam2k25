using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Enemy boss;

    private void Start()
    {
        if (boss != null)
        {
            healthSlider.maxValue = boss.health;
            healthSlider.value = boss.health;
            LeanTween.scale(healthSlider.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeInOutSine);
        }
    }

    private void Update()
    {
        if (boss != null)
        {
            healthSlider.value = boss.health;
        }

        if (boss.health <= 0)
        {
            AudioManager.instance.Play("bossDeath");
            healthSlider.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            AudioManager.instance.Play("bossSpawn");
            healthSlider.gameObject.SetActive(true);
        }
    }
}
