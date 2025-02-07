using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{
    [SerializeField] private bora movement;
    [SerializeField] private BetterPlayerAttack attack;
    public void DisablePlayerMovement()
    {
        movement.enabled = false;
        attack.enabled = false;
    }

    public void EnablePlayerMovement()
    {
        movement.enabled = true;
        attack.enabled = true;
    }
}
