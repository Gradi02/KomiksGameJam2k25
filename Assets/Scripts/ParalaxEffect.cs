using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float speed;


    void Start()
    {
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dst = cam.transform.position.x * speed;

        transform.position = new Vector3 (startPos + dst, transform.position.y, transform.position.z);
    }
}
