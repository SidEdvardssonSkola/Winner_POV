using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 25;
    [SerializeField] private CameraShake screenShake;
    private float actualDamage;

    [SerializeField] private AudioSource clang;

    private void Start()
    {
        actualDamage = damage;
    }

    public void UpgradeDamage(int strengthLevel)
    {
        actualDamage = damage * Mathf.Pow(1.2f, strengthLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            screenShake.ShakeScreen(0.2f, 0.25f);
            collision.gameObject.GetComponent<IDamageable>().ChangeHealth(-actualDamage);
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            screenShake.ShakeScreen(0.1f, 0.15f);
            collision.gameObject.GetComponent<IDamageable>().ChangeHealth(-actualDamage);
        }

        if (!collision.gameObject.CompareTag("Player") && collision.isTrigger == false)
        {
            clang.pitch = Random.Range(0.75f, 1.25f);
            clang.Play();
        }
    }
}
