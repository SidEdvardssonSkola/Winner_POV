using UnityEngine;

public class SlopeSpeedBoost : MonoBehaviour
{
    [Header("Slope Settings")]
    public LayerMask slopeLayer;         // Layer for slopes
    public float maxFallSpeedBoost = 15f; // Maximum speed boost when landing near the slope's edge
    public float edgeDistanceThreshold = 2f; // Distance from the edge for maximum boost (adjust this!)

    [Header("Debugging")]
    public bool enableDebugging = true;   // Toggle debugging visuals/logs
    public Color slopeDebugColor = Color.green; // Color for slope debug lines
    public Color edgeDebugColor = Color.blue;   // Color for edge debug lines

    private Rigidbody2D rb;             // Player's Rigidbody2D
    private bool isOnSlope = false;     // Is the player currently on a slope?
    private Collider2D currentSlope;    // The collider of the slope we’re standing on

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we collided with a slope
        if (IsInLayerMask(collision.gameObject.layer, slopeLayer))
        {
            isOnSlope = true;
            currentSlope = collision.collider;

            if (enableDebugging)
            {
                Debug.Log("Standing on a slope!");
            }

            // Calculate the edge-boost mechanic
            CalculateSlopeBoost(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Exit slope detection
        if (collision.collider == currentSlope)
        {
            isOnSlope = false;
            currentSlope = null;

            if (enableDebugging)
            {
                Debug.Log("Left the slope.");
            }
        }
    }

    private void CalculateSlopeBoost(Collision2D collision)
    {
        // Get the slope's bounds
        Bounds slopeBounds = collision.collider.bounds;

        // Calculate player's horizontal position relative to the slope
        float playerX = transform.position.x;
        float slopeLeftEdge = slopeBounds.min.x;
        float slopeRightEdge = slopeBounds.max.x;

        // Debug: Draw slope bounds
        if (enableDebugging)
        {
            Debug.DrawLine(new Vector3(slopeLeftEdge, slopeBounds.min.y, 0), new Vector3(slopeRightEdge, slopeBounds.min.y, 0), slopeDebugColor, 0.5f);
        }

        // Determine how close the player is to the bottom-right edge
        float distanceToEdge = Mathf.Abs(playerX - slopeRightEdge);
        float distanceFactor = Mathf.Clamp01(1f - (distanceToEdge / edgeDistanceThreshold)); // Closer to 0 = near edge

        // Debug: Log distance values
        if (enableDebugging)
        {
            Debug.Log($"Player X: {playerX}, Slope Right Edge: {slopeRightEdge}, Distance to Edge: {distanceToEdge}, Distance Factor: {distanceFactor}");
        }

        // Ensure fall speed is sufficient for boost
        float fallSpeed = Mathf.Abs(rb.velocity.y); // Use player's vertical fall speed
        if (fallSpeed <= 0.1f) // Small threshold to avoid applying boost when not falling
        {
            if (enableDebugging)
            {
                Debug.Log("Not enough fall speed to apply a boost.");
            }
            return;
        }

        // Calculate the speed boost
        float fallSpeedBoost = Mathf.Clamp(fallSpeed * distanceFactor, 0, maxFallSpeedBoost);

        // Debug: Log final speed boost
        if (enableDebugging)
        {
            Debug.Log($"Calculated Fall Speed: {fallSpeed}, Applied Speed Boost: {fallSpeedBoost}");
        }

        // Boost player speed in the slope's direction
        Vector2 slopeDirection = Vector2.right; // Assume slopes are slanted to the right by default
        if (slopeLeftEdge > slopeRightEdge) slopeDirection = Vector2.left; // Adjust direction if the slope faces left

        rb.velocity = new Vector2(slopeDirection.x * fallSpeedBoost, rb.velocity.y);

        // Debug: Log slope direction and applied velocity
        if (enableDebugging)
        {
            Debug.Log($"Boost Applied in Direction {slopeDirection}, Final Velocity: {rb.velocity}");
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) > 0);
    }
}
