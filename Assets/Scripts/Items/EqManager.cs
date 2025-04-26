using UnityEngine;
using UnityEngine.UI;

public class EqManager : MonoBehaviour
{
    public GameObject[] slotsBg;
    public GameObject[] LoadSlots;
    public Image[] images;
    private Item[] items = new Item[3];

    private bool[] activeItems = new bool[3];
    private int currentID = 0;
    private int maxID = 2;

    [SerializeField] private PewPewBomb bomb;
    [SerializeField] private PewPewGravity gravity;
    [SerializeField] private PewPewLaser laser;
    [SerializeField] private PewPewShotgun gun;
    [SerializeField] private PlayerAttack snake;

    private void Start()
    {
        foreach (var slot in slotsBg){
            if (slot != slotsBg[currentID]){
                slot.SetActive(false);
            }
        }
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
            SwitchWeapon();
            UpdateSlotsBg();
        }
        else if (scroll < 0f)
        {
            currentID = (currentID - 1 + (maxID + 1)) % (maxID + 1);
            SwitchWeapon();
            UpdateSlotsBg();
        }
    }

    private void UpdateSlotsBg()
    {
        foreach (var slot in slotsBg){
            if (slot != slotsBg[currentID]){
                slot.SetActive(false);
            }
        }
        slotsBg[currentID].SetActive(true);
    }

    public void PickupSign(Item itm)
    {
        images[currentID].sprite = itm.sprite;
        images[currentID].color = Color.white;
        items[currentID] = itm;
        AnimateNewRune(currentID);
    }

    private void AnimateNewRune(int i)
    {
        LeanTween.rotate(LoadSlots[i].gameObject, LoadSlots[i].transform.rotation.eulerAngles + new Vector3(0, 0, 90), 0.5f).setEase(LeanTweenType.easeInOutCubic);
    }

    private void SwitchWeapon()
    {
        bomb.enabled = false;
        gravity.enabled = false;
        laser.enabled = false;
        gun.enabled = false;
        snake.enabled = false;

        if (items[currentID] != null)
        {
            switch (items[currentID].name)
            {
                case "bomb":
                    {
                        bomb.enabled = true;
                        break;
                    }
                case "gravity":
                    {
                        gravity.enabled = true;
                        break;
                    }
                case "laser":
                    {
                        laser.enabled = true;
                        break;
                    }
                case "gun":
                    {
                        gun.enabled = true;
                        break;
                    }
                case "snake":
                    {
                        snake.enabled = true;
                        break;
                    }
            }
        }
    }
}
