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
        //Alternatywny tryb znakówo
        // switch(Input.GetKeyDown())
        // {
        //     case KeyCode.Alpha1:
        //         SwitchSlot(1);
        //         break;

        //     case KeyCodSlot(2);
        //         break
            
        //    e.Alpha2:
        //         Switch case KeyCode.Alpha3:
        //         SwitchSlot(3);
        //         break
        // }
    }

    // private void SwitchSlot(int slotNr)
    // {

    // }

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
        // slotsBg[currentID].SetActive(true);
    }
    public void DeleteSign(int itemNr = currentID)
    {
        images[currentID].sprite = itm.sprite;
        images[currentID].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        items[currentID] = itm;
    }

    public void PickupSign(Item itm)
    {
        Debug.Log(images[currentID].sprite);
        Debug.Log(images[currentID].color);
        images[currentID].sprite = null;
        images[currentID].color = Color.white;
        items[currentID] = null;

        /*if(currentID >= 3)
        {
            Debug.Log("ULT!!!!!!!!!!!!!");
            currentID = 0;
            items = new Item[3];
            foreach(var img in images)
            {
                Color a = Color.white;
                a.a = 0;
                img.color = a;
                img.sprite = null;

                images[0].transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                images[1].transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                images[2].transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }*/
    }

    private void AnimateNewRune(int i)
    {
        LeanTween.rotate(LoadSlots[i].gameObject, LoadSlots[i].transform.rotation.eulerAngles + new Vector3(0, 0, 90), 0.5f).setEase(LeanTweenType.easeInOutCubic);
    }

    private void SwitchSign(int itemNr)
    {
        if (items[itemNr] != null)
        {
            switch (items[itemNr].name)
            {
                case "bomb":
                    {
                        bomb.enabled = bomb.enabled == true ? false : true;
                        break;
                    }
                case "gravity":
                    {
                        gravity.enabled = gravity.enabled == true ? false : true;
                        break;
                    }
                case "laser":
                    {
                        laser.enabled = laser.enabled == true ? false : true;
                        break;
                    }
                case "gun":
                    {
                        gun.enabled = gun.enabled == true ? false : true;
                        break;
                    }
                case "snake":
                    {
                        snake.enabled = snake.enabled == true ? false : true;
                        break;
                    }
            }
        }
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
