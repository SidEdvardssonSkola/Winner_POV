using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 25;
    [SerializeField] private CameraShake screenShake;
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
            screenShake.ShakeScreen(0.2f, 0.25f);
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-actualDamage);
        }
    }
}
