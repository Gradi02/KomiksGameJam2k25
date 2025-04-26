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
    private float dashingPower = 24f;
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

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }

        if (isJumping && IsGrounded() && rb.linearVelocity.y < 0.01f)
        {
            StartCoroutine(EndJump());
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private IEnumerator EndJump()
    {
        yield return new WaitForSeconds(1f); // lub wiêcej, jeœli trzeba
        isJumping = false;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
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
        // U¿ywamy rb.velocity.x, poniewa¿ lepiej odzwierciedla faktyczny ruch ni¿ 'horizontal'
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);
        bool isGrounded = IsGrounded();

        // Ustawianie parametrów w Animatorze
        animator.SetFloat("speed", currentHorizontalSpeed);
        animator.SetBool("jump", isGrounded);
        animator.SetFloat("verticalvel", Mathf.Abs(rb.linearVelocity.y));

        // --- Komentarz: Jak skonfigurowaæ przejœcia w Animatorze (przyk³ady) ---
        // 1. Idle -> Walk: Warunek: Speed > 0.1 AND IsGrounded == true
        // 2. Walk -> Idle: Warunek: Speed < 0.1 AND IsGrounded == true
        // 3. Any State -> Jump: Warunek: VerticalVelocity > 0.1 AND IsGrounded == false
        // 4. Any State -> Fall: Warunek: VerticalVelocity < -0.1 AND IsGrounded == false
        // 5. Fall -> Idle/Walk: Warunek: IsGrounded == true (Mo¿na dodaæ animacjê l¹dowania)
        // 6. Any State -> Dash: Warunek: IsDashing == true
        // 7. Dash -> Idle/Fall: Warunek: IsDashing == false
        // -----------------------------------------------------------------------
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
