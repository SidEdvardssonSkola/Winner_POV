using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnterStateOnCollision : MonoBehaviour
{
    [SerializeField] private int _enterState = 1;
    [SerializeField] private int _exitState = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnDrawGizmosSelected()
    {
        switch (_enterState)
        {
            case 0:
                Gizmos.color = Color.white;
                break;

            case 1:
                Gizmos.color = Color.yellow;
                break;

            case 2:
                Gizmos.color = Color.red;
                break;
        }
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * transform.lossyScale.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.SetStatus(_enterState, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.SetStatus(_exitState, true);
        }
    }
}
