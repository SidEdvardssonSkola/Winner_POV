using UnityEngine;

public class fenemymove : MonoBehaviour
{
    public float speed = 2f; // Overall movement speed
    public float aggroRange = 10f; // Aggro range for the enemy to start chasing
    public float chaseDelay = 1f; // Delay before chasing starts again
    public float xOffset = 3f; // Distance offset on the X-axis from the player

    private GameObject player; // Reference to the player
    private bool isChasing = false; // Whether the enemy is chasing
    public Transform startingpoint; // Starting position of the enemy
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


        // Enable chase if player is within aggro range and delay has passed
        isChasing = distanceToPlayer <= aggroRange;

        if (isChasing)
            Chase();
        else
            ReturnStartPoint();

        Flip();

        rb.velocity = Vector2.zero;
    }

    private Vector2 targetPosition = new Vector2();
    private void Chase()
    {
        float deltaTime = Time.deltaTime;

        // Calculate target position with an X-axis offset from the player
        targetPosition.Set(player.transform.position.x, player.transform.position.y);
        targetPosition.x += (player.transform.position.x > transform.position.x ? -xOffset : xOffset);

        // Smooth movement towards the target position
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void ReturnStartPoint()
    {
        // Smooth movement back to the starting position
        Vector2 newPosition = Vector2.MoveTowards(transform.position, startingpoint.position, speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
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
