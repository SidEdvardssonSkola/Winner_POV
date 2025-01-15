using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Movement speed for X-axis
    public float yAlignSpeed = 3f; // Speed for Y-axis movement
    public float aggroRange = 10f; // Aggro range for the enemy to start chasing
    public float stopDistance = 1f; // Distance to stop from the player
    public Transform startingPoint; // Starting position of the enemy
    public float chaseDelay = 1f; // Delay before chasing starts again

    private GameObject player; // Reference to the player
    private bool isChasing = false; // Whether the enemy is chasing
    private float lastPlayerMoveTime; // Timestamp of the last significant player movement

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
       // {
            lastPlayerMoveTime = Time.time;
        //}
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate the distance between the player and the enemy
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


        // Check if the player has moved significantly to enable chasing
        if (distanceToPlayer <= aggroRange && Time.time >= lastPlayerMoveTime + chaseDelay)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > aggroRange)
        {
            isChasing = false;
        }

        // Perform chasing or return to start point based on the situation
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            ReturnToStartingPoint();
        }

        FlipTowardsPlayer(); // Flip the enemy to face the player

    }


    // Handle chasing the player
    public void SetChase(bool value)
    {
        isChasing = value;
    }
    private void ChasePlayer()
    {
        Vector2 playerOffset = player.transform.position - transform.position;
        var targetPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        if (playerOffset.x > 0)
        {
            targetPos.x -= stopDistance;
        }
        else
        {
            targetPos.x += stopDistance;
        }

        var targetOffset = targetPos - transform.position;

        // Calculate the direction to the player on both X and Y axes simultaneously
        Vector2 directionToPlayer = targetOffset.normalized;

        // Move the enemy towards the player on both axes
        Vector3 movement = new Vector3(directionToPlayer.x * speed * Time.deltaTime, directionToPlayer.y * yAlignSpeed * Time.deltaTime, 0);

        transform.position += movement;

        //// Stop moving towards the player on the X-axis if within stopDistance
        //if (Mathf.Abs(transform.position.x - player.transform.position.x) > stopDistance)
        //{
        //    transform.position += movement;
        //}
        //else
        //{
        //    // If too close on X, only move on Y axis to align vertically
        //    transform.position += new Vector3(0, movement.y, 0);
        //}

        // Update the last player move time
        lastPlayerMoveTime = Time.time;
    }

    // Return to the starting point if the player isn't in range
    private void ReturnToStartingPoint()
    {
        Vector2 directionToStart = (startingPoint.position - transform.position).normalized;
        Vector3 movement = new Vector3(directionToStart.x * speed * Time.deltaTime, directionToStart.y * yAlignSpeed * Time.deltaTime, 0);
        transform.position += movement;
    }

    // Flip the enemy to face the player
    private void FlipTowardsPlayer()
    {
        if (player == null) return;

        // Flip based on the X position of the player relative to the enemy
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face left
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Face right
        }
    }
}
