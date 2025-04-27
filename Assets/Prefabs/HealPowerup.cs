using UnityEngine;

public class HealPowerup : EnvSignInteractionAction
{
    private Item thisItem;

    public override void Play(EqManager eq)
    {
        Destroy(gameObject);
        AudioManager.instance.Play("pickup");

        eq.GetComponent<HealthManager>().Heal(30);
    }
}
