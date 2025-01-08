using UnityEngine;

public class bora : MonoBehaviour
{
    [Header("Basic Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    
    [Header("Wall Jump")]
    public float wallJumpForce = 8f;
    public float wallSlideSpeed = 2f;
    public int maxWallJumps = 2;
    
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isWallSliding;
    private int wallJumpsLeft;
    private bool canDash = true;
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownTimer;
    private int facingDirection = 1;

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

        // Movement and Running
        float moveX = Input.GetAxisRaw("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveX * currentSpeed, rb.velocity.y);

        // Update facing direction
        if (moveX != 0)
            facingDirection = (int)Mathf.Sign(moveX);

        // Normal Jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Wall Jump
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            
            if (Input.GetKeyDown(KeyCode.Space) && wallJumpsLeft > 0)
            {
                rb.velocity = new Vector2(-facingDirection * wallJumpForce, jumpForce);
                wallJumpsLeft--;
            }
        }

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

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check for wall sliding
        if (!canJump && Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f)
        {
            isWallSliding = true;
            wallJumpsLeft = maxWallJumps;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            canJump = true;
            isWallSliding = false;
            wallJumpsLeft = maxWallJumps;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
        isWallSliding = false;
    }
}