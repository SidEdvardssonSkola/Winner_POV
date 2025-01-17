using UnityEngine;

public class genemymovement : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the enemy
    public float aggroRange = 10f; // Range within which the enemy will start chasing
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
            MoveTowardsPlayer(); // Move the enemy towards the player
    }

    public float direction;
    private void MoveTowardsPlayer()
    {
        // Get the X direction to move in
        direction = player.transform.position.x > transform.position.x ? 1 : -1;

        // Move the enemy towards the player along the X-axis
        transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
