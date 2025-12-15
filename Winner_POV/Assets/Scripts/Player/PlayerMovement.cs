using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic")]
    private int moveDirection = 0;
    private int facingDirection = 0;
    public int FacingDirection
    {
        get 
        {
            return facingDirection; 
        }
    }
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float airControl = 1.5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int jumps = 2;
    private int remainingJumps = 2;
    private bool isAirBorne = false;

    public UnityEvent OnJump;

    [Header("Dashing")]
    bool isDashing = false;
    private Vector2 dashDirection = new();
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashDuration = 0.2f;
    private float dashTimer = 0;
    private float dashCooldownTimer = 0;
    [SerializeField] private float dashCooldown = 0.5f;

    [Header("Animations")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingJumps = jumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            remainingJumps = jumps;
            isAirBorne = false;
        }
    }

    private void Update()
    {
        ProcessInput();
        ProcessAnimations();
    }

    void FixedUpdate()
    {
        ProcessDashing();
        ProcessMovement();
    }

    private void ProcessInput()
    {
        moveDirection = (int)Input.GetAxisRaw("Horizontal");
        if (moveDirection != 0) facingDirection = moveDirection;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;

            dashDirection = new(facingDirection, 0);

            isDashing = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0 && !isDashing)
        {
            remainingJumps -= 1;
            Jump();
        }
    }

    private void ProcessMovement()
    {
        if (isDashing) return;

        if (isAirBorne)
        {
            rb.AddForce(new(moveDirection * movementSpeed * airControl, 0), ForceMode2D.Force);
            rb.velocity = new(Mathf.Clamp(rb.velocity.x, -movementSpeed, movementSpeed), rb.velocity.y);
            return;
        }

        rb.velocity = new(moveDirection * movementSpeed, rb.velocity.y);
    }

    private void ProcessDashing()
    {
        if (isDashing)
        {
            rb.velocity = dashSpeed * dashDirection;

            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer < 0)
            {
                isDashing = false;
                dashTimer = dashCooldown;
            }
        }
        else
        {
            dashCooldownTimer -= Time.fixedDeltaTime;
        }
    }

    private void ProcessAnimations()
    {
        playerSpriteRenderer.flipX = facingDirection < 0;

        playerAnimator.SetBool("IsRun", Mathf.Abs(rb.velocity.x) > 0.1f && !isAirBorne);
        playerAnimator.SetBool("IsJump", isAirBorne);
        playerAnimator.SetBool("IsDash", isDashing);
        playerAnimator.SetBool("IsIdle", Mathf.Abs(rb.velocity.x) < 0.1f && !isAirBorne);

        playerAnimator.SetBool("IsFacingLeft", facingDirection < 0);
        playerAnimator.SetBool("IsFacingRight", facingDirection > 0);
    }

    public void Jump()
    {
        OnJump.Invoke();

        rb.velocity = new(rb.velocity.x, jumpForce);
        isAirBorne = true;
    }
}