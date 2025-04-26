using UnityEngine;
using UnityEngine.UI;

public class EqManager : MonoBehaviour
{
    public GameObject[] slotsBg;
    public GameObject[] LoadSlots;
    public Image[] images;
    private Item[] items = new Item[3];

    private int currentID = 0;
    private int maxID = 2;

    private void Start()
    {
        foreach (var slot in slotsBg)
            slot.SetActive(false);

        slotsBg[currentID].SetActive(true);
    }

    private void Update()
    {
        HandleScroll();
    }

    private void HandleScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentID = (currentID + 1) % (maxID + 1);
            UpdateSlotsBg();
        }
        else if (scroll < 0f)
        {
            currentID = (currentID - 1 + (maxID + 1)) % (maxID + 1);
            UpdateSlotsBg();
        }
    }

    private void UpdateSlotsBg()
    {
        foreach (var slot in slotsBg)
            slot.SetActive(false);

        slotsBg[currentID].SetActive(true);
    }

    public void PickupItem(Item itm)
    {
        images[currentID].sprite = itm.sprite;
        images[currentID].color = Color.white;
        items[currentID] = itm;
    }
}
