using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 25;
    private float actualDamage;

    private void Start()
    {
        actualDamage = damage;
    }

    public void UpgradeDamage(int strengthLevel)
    {
        actualDamage = damage * Mathf.Pow(1.2f, strengthLevel);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-actualDamage);
        }
    }
}
