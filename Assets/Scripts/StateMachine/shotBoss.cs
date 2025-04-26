using UnityEngine;

public class shotBoss : State
{
    [SerializeField] private float delayToShot = 0.3f;
    [SerializeField] private float timeToEnd = 0.5f;
    private float t1, t2;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSp;
    private bool shoted;

    public override void StartState()
    {
        base.StartState();
        t1 = Time.time + delayToShot;
        t2 = Time.time + timeToEnd;
        shoted = false;
        rb.linearVelocity = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Time.time > t1 && !shoted)
        {
            shoted = true;
            Shot();
        }

        if (Time.time > t2)
        {
            isEnded = true;
            machine.RequestExitState();
        }
    }

    public override void EndState()
    {
        base.EndState();
    }



    public void Shot()
    {
        GameObject a = Instantiate(arrow, arrowSp.transform.position, transform.rotation);
        a.GetComponent<arrowMovement>().InvokeDestroy(4);
    }
}
