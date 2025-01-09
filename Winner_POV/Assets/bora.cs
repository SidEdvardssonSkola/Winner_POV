using UnityEngine;

public class bora : MonoBehaviour
{
    [Header("Basic Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    [Header("Wall Jump")]
    public float wallJumpForce = 10f;
    public float wallSlideSpeed = 2f;
    public float wallJumpDirectionForce = 8f;
    public float wallJumpMomentumPreservation = 0.95f; // Multiplier for momentum preservation

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isWallSliding;
    private bool canWallJump = false;
    private bool canDash = true;
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownTimer;
    private int facingDirection = 1;

    private bool overrideVelocity; // Flag to prevent movement overriding

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            HandleDash();
            return;
        }

        if (!overrideVelocity) // Skip movement updates when overriding velocity
        {
            // Movement and Running
            float moveX = Input.GetAxisRaw("Horizontal");
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
            rb.velocity = new Vector2(moveX * currentSpeed, rb.velocity.y);

            // Update facing direction
            if (moveX != 0)
                facingDirection = (int)Mathf.Sign(moveX);
        }

        // Normal Jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Wall Jump
        HandleWallJump(Input.GetAxisRaw("Horizontal"));

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            StartDash();
        }

        // Update dash cooldown
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
        else
            canDash = true;
    }

    void HandleWallJump(float moveX)
    {
        if (isWallSliding)
        {
            bool holdingTowardsWall = (moveX * facingDirection) > 0;

            if (holdingTowardsWall)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            }

            if (Input.GetKeyDown(KeyCode.Space) && canWallJump)
            {
                // Jump away from the wall
                ApplyVelocity(new Vector2(-facingDirection * wallJumpDirectionForce, wallJumpForce), preserveMomentum: true);
                canWallJump = false;
                isWallSliding = false;
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration;
        dashCooldownTimer = dashCooldown;
        rb.gravityScale = 0;
    }

    void HandleDash()
    {
        if (dashTimeLeft > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// Applies a specific velocity to the character, with an option to preserve momentum.
    /// </summary>
    /// <param name="velocity">The velocity to apply.</param>
    /// <param name="preserveMomentum">Whether to preserve the character's momentum.</param>
    void ApplyVelocity(Vector2 velocity, bool preserveMomentum = false)
    {
        if (preserveMomentum)
        {
            velocity.x *= wallJumpMomentumPreservation;
            velocity.y *= wallJumpMomentumPreservation;
        }

        rb.velocity = velocity;
        overrideVelocity = true;

        // Reset override flag after a short delay
        Invoke(nameof(ResetOverride), 0.2f);
    }

    /// <summary>
    /// Resets the velocity override flag.
    /// </summary>
    void ResetOverride()
    {
        overrideVelocity = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check for wall sliding
        if (!canJump && Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f)
        {
            isWallSliding = true;
            canWallJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            canJump = true;
            isWallSliding = false;
            canWallJump = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset states when leaving surfaces
        canJump = false;
        isWallSliding = false;
        canWallJump = false;
    }
}
