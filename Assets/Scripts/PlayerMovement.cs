using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private bool isTowardsRight = true;

    private bool canDash = true;
    private bool isDashing, isJumping;
    private float dashingPower = 25f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private Animator animator;

    void Update()
    {
        UpdateAnimations();

        if (isDashing) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (!(Mathf.Abs(horizontal) > 0.1f && IsGrounded()))
        {
            AudioManager.instance.PlayLoop("run");
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            AudioManager.instance.Play("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }

        if (isJumping && IsGrounded() && rb.linearVelocity.y < 0.01f)
        {
            StartCoroutine(EndJump());
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.4f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private IEnumerator EndJump()
    {
        yield return new WaitForSeconds(1f); // lub wi�cej, je�li trzeba
        isJumping = false;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isTowardsRight && horizontal < 0f || !isTowardsRight && horizontal > 0f)
        {
            isTowardsRight = !isTowardsRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void UpdateAnimations()
    {
        // U�ywamy rb.velocity.x, poniewa� lepiej odzwierciedla faktyczny ruch ni� 'horizontal'
        float currentHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        bool isGrounded = IsGrounded();

        // Ustawianie parametr�w w Animatorze
        animator.SetFloat("speed", currentHorizontalSpeed);
        animator.SetBool("jump", isGrounded);
        animator.SetFloat("verticalvel", Mathf.Abs(rb.linearVelocity.y));
    }


    private IEnumerator Dash()
    {
        AudioManager.instance.Play("dash");
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        Vector2 dashDirection = (mousePosition - transform.position).normalized;
        Debug.Log(dashDirection);

        rb.linearVelocity = dashDirection * dashingPower;

        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
