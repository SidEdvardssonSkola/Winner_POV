using UnityEngine;

public class fenemymove : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float stopDistance = 1f; // Distance to stop from the player
    public float yAlignSpeed = 3f; // Speed for aligning on the Y-axis
    public float aggroRange = 10f; // Aggro range for the enemy to start chasing

    private GameObject player; // Reference to the player
    public bool chase = false; // Whether the enemy is chasing
    public Transform startingpoint; // Starting position of the enemy

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Enable chase if player is within aggro range
        chase = distanceToPlayer <= aggroRange;

        if (chase)
            Chase();
        else
            ReturnStartPoint();

        Flip();
    }

    private void Chase()
    {
        float deltaTime = Time.deltaTime;

        // Align Y position first
        if (Mathf.Abs(transform.position.y - player.transform.position.y) > 0.1f)
        {
            float yDirection = player.transform.position.y > transform.position.y ? 1 : -1;
            transform.position += new Vector3(0, yAlignSpeed * yDirection * deltaTime, 0);
        }
        else
        {
            // After aligning Y, approach on the X-axis if not within stopDistance
            float distanceToPlayerX = Mathf.Abs(transform.position.x - player.transform.position.x);

            if (distanceToPlayerX > stopDistance)
            {
                float xDirection = player.transform.position.x > transform.position.x ? 1 : -1;
                transform.position += new Vector3(speed * xDirection * deltaTime, 0, 0);
            }
        }
    }

    private void ReturnStartPoint()
    {
        // Return to the starting position
        transform.position = Vector2.MoveTowards(transform.position, startingpoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        // Flip sprite to face the player
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