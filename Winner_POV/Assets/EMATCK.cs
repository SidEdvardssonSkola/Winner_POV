using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damageAmount = -10; // Damage dealt to the player (negative for damage)
    public float attackRange = 1.5f; // Range within which the enemy can attack
    public float attackCooldown = 1.0f; // Time between attacks

    private float lastAttackTime;

    void Update()
    {
        // Check for the player within range and attack if possible
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange);
        if (hitCollider.CompareTag("Player"))
        {
            TryAttack(hitCollider.gameObject);
        }
    }

    private void TryAttack(GameObject player)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(damageAmount);
                lastAttackTime = Time.time; // Reset the attack cooldown
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
