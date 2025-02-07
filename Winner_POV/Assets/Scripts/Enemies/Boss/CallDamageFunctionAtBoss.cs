using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDamageFunctionAtBoss : MonoBehaviour
{
    [SerializeField] private float damagePercentage = 1f;
    [SerializeField] private bool ignoreIFrames = false;
    [SerializeField] private bool isDPS = false;

    private AttackBase boss;

    private void Start()
    {
        boss = FindFirstObjectByType<Boss>().GetComponent<Enemy>().attackBaseReference;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isDPS)
            {
                boss.DamagePlayer(damagePercentage * Time.deltaTime, ignoreIFrames);
                return;
            }
            boss.DamagePlayer(damagePercentage, ignoreIFrames);
        }
    }
}
