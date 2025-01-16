using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerMovementWhileActive : MonoBehaviour
{
    [SerializeField] private DisableMovement player;
    private void OnEnable()
    {
        player.DisablePlayerMovement();
    }

    private void OnDisable()
    {
        player.EnablePlayerMovement();
    }
}
