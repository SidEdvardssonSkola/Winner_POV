using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicMovement
{
    Rigidbody2D Rb { get; set; }
    float Speed { get; set; }
    float Acceleration { get; set; }

    bool IsFacingRight { get; set; }

    void AddVelocity(Vector2 velocityToAdd);
    void CheckIfFacingLeftOrRight(float xVelocity);
    IEnumerator DampVelocityToCap();
}
