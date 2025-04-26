using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private crossbowBoss boss;

    private void Start()
    {
        if (boss != null)
        {
            healthSlider.maxValue = boss.health;
            healthSlider.value = boss.health;
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
            healthSlider.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
    }
}
