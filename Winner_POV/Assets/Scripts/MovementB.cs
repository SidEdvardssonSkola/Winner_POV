using UnityEngine;
using TMPro;

public class bora : MonoBehaviour
{
    [Header("Basic Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 10f;

    [Header("Wall Jump")]
    public float wallJumpForce = 10f;
    public float wallSlideSpeed = 2f;
    public float wallJumpDirectionForce = 8f;
    public float momentumDuration = 0.2f;
    public float momentumControlMultiplier = 0.3f;
    public float wallJumpBoostSpeed = 10f;
    public float wallKeyDisableTime = 0.5f;

    [Header("Air Control")]
    public float airControlForce = 40f;
    public float airMaxSpeed = 8f;

    [Header("Fast Fall")]
    public float fastFallMultiplier = 2f;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Animation")]
    public Animator animator; // Animator to control animations

    [Header("UI")]
    public TextMeshProUGUI velocityDisplay;

    [Header("References")]
    public SpriteRenderer spriteRenderer; // Drag your sprite renderer here

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isWallSliding = false;
    private bool canWallJump = false;
    private bool isDashing;

    private float dashTimeLeft;
    private float dashCooldownTimer;
    private int facingDirection = 1;

    private bool preservingMomentum;
    private float momentumTimeLeft;
    private float wallKeyDisableTimeLeft;
    private KeyCode disabledKey;

    private bool isWallLeft;
    private bool isWallRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateVelocityDisplay();

        if (isDashing)
        {
            HandleDash();
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl) && dashCooldownTimer <= 0)
        {
            StartDash();
            return;
        }

        HandleMovement();

        // Apply fast fall if holding S
        if (Input.GetKey(KeyCode.S) && rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * fastFallMultiplier * Time.deltaTime;
        }

        // Update dash cooldown
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Flip sprite based on movement direction
        if (moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0;
            facingDirection = (int)Mathf.Sign(moveX);
        }

        // Check for disabled wall-side key
        if (wallKeyDisableTimeLeft > 0)
        {
            wallKeyDisableTimeLeft -= Time.deltaTime;
            if ((disabledKey == KeyCode.A && moveX < 0) || (disabledKey == KeyCode.D && moveX > 0))
            {
                moveX = 0;
            }
        }

        // Wall jump momentum preservation
        if (preservingMomentum)
        {
            momentumTimeLeft -= Time.deltaTime;
            if (momentumTimeLeft <= 0)
            {
                preservingMomentum = false;
            }
            else
            {
                // Allow some control during momentum
                float controlForce = moveX * airControlForce * momentumControlMultiplier;
                rb.AddForce(new Vector2(controlForce, 0) * Time.deltaTime, ForceMode2D.Impulse);

                // Determine target speed based on input direction
                float targetSpeed;
                if (facingDirection > 0) // Jumped from right wall
                {
                    targetSpeed = (moveX < 0) ? wallJumpBoostSpeed : wallJumpDirectionForce;
                }
                else // Jumped from left wall
                {
                    targetSpeed = (moveX > 0) ? wallJumpBoostSpeed : wallJumpDirectionForce;
                }

                float minSpeed = targetSpeed * (momentumTimeLeft / momentumDuration);
                float currentXVel = Mathf.Abs(rb.velocity.x);
                
                if (currentXVel < minSpeed)
                {
                    rb.velocity = new Vector2(-facingDirection * minSpeed, rb.velocity.y);
                }
            }
        }

        // Ground movement
        if (canJump)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }
        else
        {
            // Air movement
            float airForce = moveX * airControlForce;
            rb.AddForce(new Vector2(airForce, 0) * Time.deltaTime, ForceMode2D.Impulse);

            // Clamp air velocity
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x, -airMaxSpeed, airMaxSpeed),
                rb.velocity.y);
        }

        HandleWallJump(moveX);

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
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

            if (Input.GetKey(KeyCode.Space) && canWallJump)
            {
                rb.velocity = new Vector2(-facingDirection * wallJumpDirectionForce, wallJumpForce);
                
                preservingMomentum = true;
                momentumTimeLeft = momentumDuration;
                
                // Disable movement towards the wall
                wallKeyDisableTimeLeft = wallKeyDisableTime;
                disabledKey = facingDirection > 0 ? KeyCode.D : KeyCode.A;
                
                isWallSliding = false;
                canWallJump = false;
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownTimer = dashCooldown;
        rb.gravityScale = 0;
        
        // Ensure animator gets updated immediately when dash starts
        UpdateAnimations();
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
            rb.velocity = new Vector2(rb.velocity.x, 0);
            
            // Ensure animator gets updated when dash ends
            UpdateAnimations();
        }
    }

    void UpdateVelocityDisplay()
    {
        if (velocityDisplay)
        {
            velocityDisplay.text = $"Velocity: X = {rb.velocity.x:F2}, Y = {rb.velocity.y:F2}";
        }
    }

    void UpdateAnimations()
    {
        if (animator == null) return;

        animator.SetBool("IsRun", Mathf.Abs(rb.velocity.x) > 0.1f && canJump);
        animator.SetBool("IsJump", !canJump);
        animator.SetBool("IsDash", isDashing);
        animator.SetBool("IsIdle", Mathf.Abs(rb.velocity.x) < 0.1f && canJump);
        
        // Normal facing direction
        animator.SetBool("IsFacingLeft", facingDirection < 0);
        animator.SetBool("IsFacingRight", facingDirection > 0);
        
        // Wall slide based on wall position
        animator.SetBool("IsWallSlideLeft", isWallSliding && isWallLeft);
        animator.SetBool("IsWallSlideRight", isWallSliding && isWallRight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the ground
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            canJump = true;
            isWallSliding = false; // Reset wall sliding if grounded
            canWallJump = false;   // Reset wall jumping if grounded
            preservingMomentum = false;

            isWallLeft = false;    // Reset wall states
            isWallRight = false;

            UpdateAnimations();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset states when leaving a surface
        canJump = false;
        isWallSliding = false;
        isWallLeft = false;
        isWallRight = false;

        UpdateAnimations();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if grounded
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            canJump = true;
            isWallSliding = false; // Prevent wall sliding while grounded
            canWallJump = false;

            isWallLeft = false;
            isWallRight = false;

            UpdateAnimations();
            return; // Exit, as we're grounded
        }

        // Check if touching a wall (only applies if off the ground)
        if (!canJump && Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f)
        {
            isWallSliding = true;
            canWallJump = true;

            // Determine which side the wall is on
            isWallLeft = collision.GetContact(0).normal.x > 0;  // Wall is on left
            isWallRight = collision.GetContact(0).normal.x < 0; // Wall is on right

            UpdateAnimations();
        }
    }
}
