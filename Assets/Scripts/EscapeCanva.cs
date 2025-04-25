using UnityEngine;
using UnityEngine.UI;

public class EscapeCanva : MonoBehaviour
{
    private KeyCode escape = KeyCode.Escape;
    private bool vis = true;
    [SerializeField] private Image[] imgs;


    void Start()
    {
        ChangeVis(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(escape))
        {
            ChangeVis(!vis);
        }
    }


    private void ChangeVis(bool i)
    {
        vis = i;
        foreach (var item in imgs)
            item.gameObject.SetActive(i);
        Time.timeScale = vis ? 0 : 1;
    }
}
