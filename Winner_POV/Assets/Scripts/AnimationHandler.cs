using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bora movement;

    void Start()
    {
        movement = GetComponent<bora>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
            
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (animator == null) return;

        // Basic movement animations
        animator.SetBool("IsRun", Mathf.Abs(movement.Velocity.x) > 0.1f && movement.IsGrounded);
        animator.SetBool("IsJump", !movement.IsGrounded);
        animator.SetBool("IsDash", movement.IsDashing);
        animator.SetBool("IsIdle", Mathf.Abs(movement.Velocity.x) < 0.1f && movement.IsGrounded);

        // Wall slide animations
        animator.SetBool("IsWallSlideLeft", movement.IsWallSliding && movement.IsWallLeft);
        animator.SetBool("IsWallSlideRight", movement.IsWallSliding && movement.IsWallRight);

        // Facing direction
        animator.SetBool("IsFacingLeft", movement.FacingDirection < 0);
        animator.SetBool("IsFacingRight", movement.FacingDirection > 0);

        // Handle sprite flipping
        if (Mathf.Abs(movement.Velocity.x) > 0.1f)
        {
            spriteRenderer.flipX = movement.Velocity.x < 0;
        }
    }
} 