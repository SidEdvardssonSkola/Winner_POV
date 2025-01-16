using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2f; // Overall movement speed
    public float aggroRange = 10f; // Aggro range for the enemy to start chasing
    public float xOffset = 3f; // Distance offset on the X-axis from the player

    private GameObject player; // Reference to the player
    private bool isChasing = false; // Whether the enemy is chasing
    public Transform startingpoint; // Starting position of the enemy
    private Rigidbody2D rb;
    private float groundY; // Y-coordinate for staying grounded

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Set the ground Y-coordinate to the enemy's starting position
        groundY = transform.position.y;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Enable chase if player is within aggro range
        isChasing = distanceToPlayer <= aggroRange;

        if (isChasing)
            Chase();
        else
            ReturnStartPoint();

        Flip();

        //rb.velocity = Vector2.zero;
    }

    private void Chase()
    {
        float deltaTime = Time.deltaTime;

        // Calculate target X position with an offset from the player
        float targetX = player.transform.position.x + (player.transform.position.x > transform.position.x ? -xOffset : xOffset);

        // Move towards the target X position, keeping the same Y-coordinate
        float newX = Mathf.MoveTowards(transform.position.x, targetX, speed * deltaTime);
        transform.position = new Vector3(newX, groundY, transform.position.z);
    }

    private void ReturnStartPoint()
    {
        // Move back to the starting X position, keeping the same Y-coordinate
        float newX = Mathf.MoveTowards(transform.position.x, startingpoint.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(newX, groundY, transform.position.z);
    }

    private void Flip()
    {
        // Flip sprite to face the player
        if (player == null) return;

        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
