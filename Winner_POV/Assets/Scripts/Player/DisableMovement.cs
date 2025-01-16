using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{
    [SerializeField] private bora movement;
    public void DisablePlayerMovement()
    {
        movement.enabled = false;
    }

    public void EnablePlayerMovement()
    {
        movement.enabled = true;
    }
}
