using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private Vector2 startPos;
    public GameObject cam;
    public float speed;
    public Sprite[] bg;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dst1 = cam.transform.position.x * speed;
        float dst2 = cam.transform.position.y * speed;

        transform.position = new Vector3 (startPos.x + dst1, startPos.y + dst2, transform.position.z);
    }

    public void Changebg()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr.sprite == bg[0])
        {
            sr.sprite = bg[1];
        }
        else
        {
            sr.sprite = bg[0];
        }
    }
}
