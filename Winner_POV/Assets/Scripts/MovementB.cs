using UnityEngine;
using UnityEngine.Events;
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
    public LayerMask enemyLayer;

    [Header("Animation")]
    public Animator animator;

    [Header("UI")]
    public TextMeshProUGUI velocityDisplay;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    [Header("Events")]
    public UnityEvent OnDash;
    public UnityEvent OnJump;
    public UnityEvent OnLand;
    public UnityEvent OnWallJump;

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isWallSliding = false;
    private bool canWallJump = false;
    private bool isDashing;

    private float dashTimeLeft;
    private float dashCooldownTimer;
    private int facingDirection = 1;
    private Vector2 dashDirection;
    private int originalLayer;

    private bool preservingMomentum;
    private float momentumTimeLeft;
    private float wallKeyDisableTimeLeft;
    private KeyCode disabledKey;

    private bool isWallLeft;
    private bool isWallRight;

    public int FacingDirection => facingDirection;
    public bool CanJump => canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;
    }

    void Update()
    {
        UpdateVelocityDisplay();

        if (isDashing)
        {
            HandleDash();
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && dashCooldownTimer <= 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            if (horizontal != 0 || vertical != 0)
            {
                dashDirection = new Vector2(horizontal, vertical).normalized;
            }
            else
            {
                dashDirection = new Vector2(facingDirection, 0);
            }
            
            StartDash();
            return;
        }

        HandleMovement();

        if (Input.GetKey(KeyCode.S) && rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * fastFallMultiplier * Time.deltaTime;
        }

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        if (moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0;
            facingDirection = (int)Mathf.Sign(moveX);
        }

        if (wallKeyDisableTimeLeft > 0)
        {
            wallKeyDisableTimeLeft -= Time.deltaTime;
            if ((disabledKey == KeyCode.A && moveX < 0) || (disabledKey == KeyCode.D && moveX > 0))
            {
                moveX = 0;
            }
        }

        if (preservingMomentum)
        {
            momentumTimeLeft -= Time.deltaTime;
            if (momentumTimeLeft <= 0)
            {
                preservingMomentum = false;
            }
            else
            {
                float controlForce = moveX * airControlForce * momentumControlMultiplier;
                rb.AddForce(new Vector2(controlForce, 0) * Time.deltaTime, ForceMode2D.Impulse);

                float targetSpeed;
                if (facingDirection > 0)
                {
                    targetSpeed = (moveX < 0) ? wallJumpBoostSpeed : wallJumpDirectionForce;
                }
                else
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

        if (canJump)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }
        else
        {
            float airForce = moveX * airControlForce;
            rb.AddForce(new Vector2(airForce, 0) * Time.deltaTime, ForceMode2D.Impulse);
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x, -airMaxSpeed, airMaxSpeed),
                rb.velocity.y);
        }

        HandleWallJump(moveX);

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            OnJump?.Invoke();
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
                
                wallKeyDisableTimeLeft = wallKeyDisableTime;
                disabledKey = facingDirection > 0 ? KeyCode.D : KeyCode.A;
                
                isWallSliding = false;
                canWallJump = false;
                OnWallJump?.Invoke();
            }
        }
    }

    public void ResetDash()
    {
        dashCooldownTimer = 0f;
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownTimer = dashCooldown;
        rb.gravityScale = 0;
        
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        
        OnDash?.Invoke();
        UpdateAnimations();
    }

    void HandleDash()
    {
        if (dashTimeLeft > 0)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.deltaTime;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                ResetDash();
                break;
            }
        }
        else
        {
            EndDash();
        }
    }

    void EndDash()
    {
        isDashing = false;
        rb.gravityScale = 1;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        
        UpdateAnimations();
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
        
        animator.SetBool("IsFacingLeft", facingDirection < 0);
        animator.SetBool("IsFacingRight", facingDirection > 0);
        
        animator.SetBool("IsWallSlideLeft", isWallSliding && isWallLeft);
        animator.SetBool("IsWallSlideRight", isWallSliding && isWallRight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            bool wasGrounded = canJump;
            canJump = true;
            isWallSliding = false;
            canWallJump = false;
            preservingMomentum = false;

            isWallLeft = false;
            isWallRight = false;

            if (!wasGrounded)
            {
                OnLand?.Invoke();
            }

            UpdateAnimations();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
        isWallSliding = false;
        isWallLeft = false;
        isWallRight = false;

        UpdateAnimations();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            canJump = true;
            isWallSliding = false;
            canWallJump = false;

            isWallLeft = false;
            isWallRight = false;

            UpdateAnimations();
            return;
        }

        if (!canJump && Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f)
        {
            isWallSliding = true;
            canWallJump = true;

            isWallLeft = collision.GetContact(0).normal.x > 0;
            isWallRight = collision.GetContact(0).normal.x < 0;

            UpdateAnimations();
        }
    }
}
