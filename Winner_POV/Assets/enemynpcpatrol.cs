using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemynpcpatrol : MonoBehaviour
{
    public GameObject pointA; // First target point
    public GameObject pointB; // Second target point
    private Rigidbody2D rb;
    private Transform currentPoint; // The point that the enemy is currently heading towards
    public float speed = 3f; // Speed of movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform; // Start moving towards pointA
    }

    void Update()
    {
        // Move the enemy towards the current target point
        MoveTowardsTarget();

        // Check if the enemy is close enough to the current point to switch targets
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            // Switch target point when close enough
            SwitchTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate direction to the current point
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        // Set the velocity to move the enemy towards the current target point
        rb.velocity = direction * speed;
    }

    private void SwitchTarget()
    {
        // Switch between pointA and pointB
        if (currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform; // Move towards pointB
        }
        else
        {
            currentPoint = pointA.transform; // Move towards pointA
        }
    }
}
