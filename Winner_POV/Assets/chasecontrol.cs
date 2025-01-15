using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasecontrol : MonoBehaviour
{
    public EnemyMovement[] enemyarray;  // Array of enemies to control

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When player enters the trigger area, make all enemies chase the player
            foreach (EnemyMovement enemy in enemyarray)
            {
                enemy.SetChase(true);  // Start chasing the player
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When player exits the trigger area, make all enemies stop chasing the player
            foreach (EnemyMovement enemy in enemyarray)
            {
                enemy.SetChase(false);  // Stop chasing the player
            }
        }
    }
}
