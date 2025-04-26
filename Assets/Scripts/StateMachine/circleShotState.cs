using UnityEngine;

public class circleShotState : State
{
    [SerializeField] private float delayToShot = 0.3f;
    [SerializeField] private float timeToEnd = 0.5f;
    private float t1, t2;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSp;
    private int numberOfBullets = 10;
    private float bulletSpeed = 5f;
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
            FireInCircle();
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



    public void FireInCircle()
    {
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 moveDirection = new Vector2(dirX, dirY).normalized;

            GameObject b = Instantiate(bullet, bulletSp.transform.position, Quaternion.identity);
            bulletMovement bm = b.GetComponent<bulletMovement>();
            bm.SetDirection(moveDirection);

            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = moveDirection * bulletSpeed;
            }

            angle += angleStep;
        }
    }
}
