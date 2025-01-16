using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    
    public void AddHealthPercent(float ammount)
    {
        playerHealth.ChangeHealth(ammount * playerHealth.maxHealth);
    }

    public void AddHealth(float ammount)
    {
        playerHealth.ChangeHealth(ammount);
    }
}
