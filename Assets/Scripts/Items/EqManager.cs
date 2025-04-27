using UnityEngine;
using UnityEngine.UI;

public class EqManager : MonoBehaviour
{
    public GameObject[] slotsBg;
    public GameObject[] LoadSlots;
    public Image[] images;
    private Item[] items = new Item[3];
    [SerializeField] private KameHameHa kame;

    public int currentID { get; private set; } = 0;
    public int maxID { get;  } = 2;

    [SerializeField] private PewPewGravity gravity;
    [SerializeField] private PewPewLaser laser;
    [SerializeField] private PewPewShotgun gun;
    [SerializeField] private PlayerAttack snake;




    public void PickupSign(Item itm)
    {
        images[currentID].sprite = itm.sprite;
        images[currentID].color = Color.white;
        items[currentID] = itm;
        AnimateNewRune(currentID);
        currentID++;
    }

    private void AnimateNewRune(int i)
    {
        LeanTween.rotate(LoadSlots[i].gameObject, LoadSlots[i].transform.rotation.eulerAngles + new Vector3(0, 0, 90), 0.5f).setEase(LeanTweenType.easeInOutCubic);
    }

    private void Update()
    {
        if(currentID > maxID)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                for(int i = 0; i < currentID; i++)
                {
                    AnimateNewRune(i);
                    images[i].sprite = null;
                    images[i].color = Color.white;
                    items[i] = null;
                }

                StartCoroutine(kame.KameHameHaCoroutine());
                currentID = 0;
            }
        }
    }

    private void SwitchWeapon()
    {
        //bomb.enabled = false;
        gravity.enabled = false;
        laser.enabled = false;
        gun.enabled = false;
        snake.enabled = false;

        if (items[currentID] != null)
        {
            switch (items[currentID].name)
            {
                /*case "bomb":
                    {
                        bomb.enabled = true;
                        break;
                    }*/
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
