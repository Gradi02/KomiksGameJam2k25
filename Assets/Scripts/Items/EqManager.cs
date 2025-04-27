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

    private float timeToEndPowerup = 0;
    private float powerupTime = 10;
    private Item power = null;
    [SerializeField] private Item defaultItem;


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
                    Color c = Color.white;
                    c.a = 0;
                    images[i].color = c;
                    items[i] = null;
                }

                StartCoroutine(kame.KameHameHaCoroutine());
                currentID = 0;
            }
        }


        if(power != defaultItem && Time.time > timeToEndPowerup)
        {
            power = defaultItem;
            SwitchWeapon(defaultItem);
            timeToEndPowerup = 0;
        }
    }



    public void ActivePowerUp(Item itm)
    {
        if(itm != null)
        {
            power = itm;
            timeToEndPowerup = Time.time + powerupTime;
            SwitchWeapon(itm);
        }
    }

    private void SwitchWeapon(Item itm)
    {
        gravity.enabled = false;
        laser.enabled = false;
        gun.enabled = false;
        snake.enabled = false;

        switch (itm.name)
        {
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
