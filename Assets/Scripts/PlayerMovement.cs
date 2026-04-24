using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 8f;
    public float jumpForce = 14f;

    [Header("Dash")]
    public float dashForce = 135f;
    public float dashTime = 0.5f;
    public float dashCooldown = 1f;

    [Header("Ataque")]
    public Transform attackHitbox;
    public float attackDistance = 1f;

    [Header("Escada")]
    public float climbSpeed = 5f;

    [Header("Chão")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;

    private bool nearLadder = false;
    private bool isClimbing = false;

    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;

        if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        // virar personagem + hitbox
        if (moveInput > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);

            if (attackHitbox != null)
                attackHitbox.localPosition = new Vector3(attackDistance, 0f, 0f);
        }

        if (moveInput < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);

            if (attackHitbox != null)
                attackHitbox.localPosition = new Vector3(-attackDistance, 0f, 0f);
        }

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0 && !isClimbing)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (nearLadder && Input.GetKey(KeyCode.W))
        {
            isClimbing = true;
        }

        if (isClimbing && Input.GetKeyDown(KeyCode.W))
        {
            isClimbing = false;
            rb.gravityScale = 3f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
            return;

        if (isClimbing)
        {
            rb.gravityScale = 0f;

            float vertical = 0f;

            if (Input.GetKey(KeyCode.W))
                vertical = 1f;

            if (Input.GetKey(KeyCode.S))
                vertical = -1f;

            rb.velocity = new Vector2(moveInput * speed, vertical * climbSpeed);

            if (!nearLadder)
            {
                isClimbing = false;
                rb.gravityScale = 3f;
            }

            return;
        }

        rb.gravityScale = 3f;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        float gravityOriginal = rb.gravityScale;
        rb.gravityScale = 0f;

        float direction = moveInput;

        if (direction == 0)
            direction = facingRight ? 1 : -1;

        rb.velocity = new Vector2(direction * dashForce, 0f);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = gravityOriginal;
        isDashing = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            nearLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            nearLadder = false;
            isClimbing = false;
            rb.gravityScale = 3f;
        }
    }
}