using UnityEngine;

public class shooting : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the enemy
    public float aggroRange = 10f; // Range within which the enemy will start chasing
    public float attackRange = 1f; // Range within which the enemy can attack
    public int attackDamage = 10; // Amount of damage dealt by the enemy's attack
    public float attackDelay = 1f; // Delay between attacks to prevent spamming
    private float lastAttackTime = 0f; // Time of the last attack

    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform shootingPoint; // The point where the projectile will be shot from

    private GameObject player; // Reference to the player
    private bool isChasing = false; // Whether the enemy is currently chasing the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
    }

    void Update()
    {
        if (player == null)
            return; // If no player is found, exit the function

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);

        // If the player is within the aggro range, start chasing
        isChasing = distanceToPlayer <= aggroRange;

        if (isChasing)
        {
            MoveTowardsPlayer(); // Move the enemy towards the player

            // If the player is within attack range and the attack delay has passed, shoot a projectile
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackDelay)
            {
                AttackPlayer(); // Attack the player by shooting a projectile
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Get the X direction to move in
        float direction = player.transform.position.x > transform.position.x ? 1 : -1;

        // Move the enemy towards the player along the X-axis
        transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void AttackPlayer()
    {
        lastAttackTime = Time.time; // Update the last attack time to prevent spamming
        Debug.Log("Enemy attacks the player!");

        // Only shoot if the player is within aggro range
        if (projectilePrefab != null && shootingPoint != null)
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        // Instantiate the projectile at the shooting point
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);

        // Get the direction towards the player on the X-axis only (ignore Y-axis movement)
        float direction = (player.transform.position.x > transform.position.x) ? 1f : -1f;

        // Apply velocity to the projectile to move it along the X-axis
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Set the velocity along the X-axis only (no vertical movement)
            rb.velocity = new Vector2(direction * 5f, 0); // 5f is the speed of the projectile
        }

        // Ensure the projectile faces the correct direction based on the enemy's position
        if (transform.position.x > player.transform.position.x)
        {
            // Flip the projectile's rotation to face left (180 degrees on the Y-axis)
            projectile.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // Keep the projectile's rotation as is (no flip)
            projectile.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Optional: You can also add a timer to destroy the projectile after a certain time
        Destroy(projectile, 5f); // Destroy projectile after 5 seconds (to prevent clutter)
    }
}
