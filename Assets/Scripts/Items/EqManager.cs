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

    [SerializeField] private PewPewBomb bomb;
    [SerializeField] private PewPewGravity gravity;
    [SerializeField] private PewPewLaser laser;
    [SerializeField] private PewPewShotgun gun;
    [SerializeField] private PlayerAttack snake;

    private void Start()
    {
        foreach (var slot in slotsBg)
            slot.SetActive(false);

        slotsBg[currentID].SetActive(true);
    }

    private void Update()
    {
        HandleScroll();
        SwitchWeapon();
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

    public void PickupSign(Item itm)
    {
        images[currentID].sprite = itm.sprite;
        images[currentID].color = Color.white;
        items[currentID] = itm;
        AnimateNewRune(currentID);

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
