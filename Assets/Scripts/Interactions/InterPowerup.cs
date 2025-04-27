using UnityEngine;

public class InterPowerup : EnvSignInteractionAction
{
    [SerializeField] private Item[] items;
    [SerializeField] private SpriteRenderer render;
    private Item thisItem;

    private void Start()
    {
        thisItem = items[Random.Range(0, items.Length)];
        render.sprite = thisItem.sprite;
    }

    public override void Play(EqManager eq)
    {
        eq.ActivePowerUp(thisItem);
        Destroy(gameObject);
        AudioManager.instance.Play("pickup");
    }
}


