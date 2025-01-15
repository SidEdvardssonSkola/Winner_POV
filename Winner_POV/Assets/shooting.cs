using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public GameObject firePoint; // GameObject for the fire point
    public float shootInterval = 2f; // Time between each shot
    public float projectileSpeed = 10f; // Speed of the projectile
    public float projectileLifetime = 5f; // Time before the projectile disappears
    public float aggroRange = 10f; // Aggro range for the enemy to start shooting
    public float shootYTolerance = 0.5f; // How much Y-axis tolerance the enemy has to be aligned with the player
    public float shootOffset = 0f; // Horizontal offset for the shooting position

    private GameObject player; // Reference to the player
    private float lastShotTime; // Time of the last shot

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null || firePoint == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        // Check if the player is within aggro range and if the enemy is aligned with the player on the Y-axis
        if (distanceToPlayer <= aggroRange && Mathf.Abs(transform.position.y - player.transform.position.y) <= shootYTolerance)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Check if enough time has passed to shoot again
        if (Time.time - lastShotTime >= shootInterval)
        {
            lastShotTime = Time.time;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

            // Calculate the base direction toward the player
            //Vector2 direction = (player.transform.position - firePoint.transform.position).normalized;
           float direction = player.transform.position.x - firePoint.transform.position.x > 0f ? 1f : -1f;

            // Set the projectile's velocity using the modified direction and speed
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                var velocity = rb.velocity;
                velocity.x = direction * projectileSpeed;
                rb.velocity = velocity;
            }

            // Destroy the projectile after its lifetime
            Destroy(projectile, projectileLifetime);
        }
    }
}
