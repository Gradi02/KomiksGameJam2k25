using UnityEngine;

public class InterEquipNewSkill : EnvSignInteractionAction
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
        if (eq.currentID <= eq.maxID)
        {
            eq.PickupSign(thisItem);
            Destroy(gameObject);

            SpawnerManager.Instance.StartWave();
        }
    }
}
