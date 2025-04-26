using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private Vector2 startPos;
    public GameObject cam;
    public float speed;


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
}
