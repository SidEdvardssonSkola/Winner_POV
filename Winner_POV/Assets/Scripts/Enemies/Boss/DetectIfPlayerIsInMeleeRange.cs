using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIfPlayerIsInMeleeRange : MonoBehaviour
{
    [SerializeField] private CircleCollider2D range;
    private bool isPlayerInRange = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range.radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            print("aaaaaaaa");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            print("eeeeeeeeee");
        }
    }

    public bool CheckForPlayer()
    {
        return isPlayerInRange;
    }
}
