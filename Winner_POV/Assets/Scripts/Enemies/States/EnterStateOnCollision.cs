using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.SetStatus(_enterState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.SetStatus(_exitState);
        }
    }
}
