using System.Collections;
using UnityEngine;

public class krabShotState : State
{
    [SerializeField] private float delayToShot = 0.3f;
    [SerializeField] private float timeToEnd = 0.5f;
    private float t1, t2;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSp;
    private bool shoted;

    public override void StartState()
    {
        Debug.Log("shot");
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
        AudioManager.instance.Play("boom");
    }

    public void Shot()
    {
        AudioManager.instance.Play("arrow");
        GameObject a = Instantiate(arrow, arrowSp.transform.position, transform.rotation);
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, 8);

        StartCoroutine(ApplyGravityAfterDelay(rb, 0.5f));
    }

    private IEnumerator ApplyGravityAfterDelay(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.gravityScale = 1;
    }
}
