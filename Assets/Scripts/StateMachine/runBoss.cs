using UnityEngine;

public class runBoss : State
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private bool reverseFlip = false;

    [SerializeField] private Transform arrowSp, arrowSp2;
    private crossbowBoss boss;

    public override void StartState()
    {
        AudioManager.instance.PlayLoop("bossGalloping");
        base.StartState();
        isEnded = true;
        boss = transform.root.GetComponent<crossbowBoss>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (playerPos - enemyPos).normalized;

        rb.linearVelocity = new Vector2(speed * dir.x, rb.linearVelocity.y);

        if (dir.x > 0.01f)
        {
            UpdateArrowSpawnerPosition(false);
            render.flipX = false;
        }   
        else
        {
            UpdateArrowSpawnerPosition(true);
            render.flipX = true;
        }

        if (reverseFlip) render.flipX = !render.flipX;

        speed = boss.phase;
    }

    public override void EndState()
    {
        AudioManager.instance.Stop("bossGalloping");
        base.EndState();
    }

    public void UpdateArrowSpawnerPosition(bool isLeft)
    {
        if (isLeft)
        {
            arrowSp.localPosition = new Vector3(-Mathf.Abs(arrowSp.localPosition.x), arrowSp.localPosition.y, arrowSp.localPosition.z);
            arrowSp2.localPosition = new Vector3(-Mathf.Abs(arrowSp.localPosition.x), arrowSp.localPosition.y, arrowSp.localPosition.z);
        }
        else
        {
            arrowSp.localPosition = new Vector3(Mathf.Abs(arrowSp.localPosition.x), arrowSp.localPosition.y, arrowSp.localPosition.z);
            arrowSp2.localPosition = new Vector3(Mathf.Abs(arrowSp.localPosition.x), arrowSp.localPosition.y, arrowSp.localPosition.z);
        }
    }
}
