using UnityEngine;

public class jumpState : State
{
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpCooldown = 0.5f;

    private bool hasJumped = false;
    private float jumpTimer = 0f;

    public override void StartState()
    {
        base.StartState();
        hasJumped = false;
        jumpTimer = 0f;

        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            hasJumped = true;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (hasJumped)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpCooldown)
            {
                isEnded = true;
            }
        }
    }

    public override void EndState()
    {
        base.EndState();
        hasJumped = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
