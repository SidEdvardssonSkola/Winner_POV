using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float xpGiveAmmount = 20;

    [SerializeField] private float maxHealth;
    private float health;
    bool isAlive = true;

    public UnityEvent onKilled;

    private CombatEncounterManager manager;
    void Start()
    {
        manager = GameObject.Find("Encounter Manager").GetComponent<CombatEncounterManager>();
        manager.AddEnemyToCounter(GetComponent<IDamageable>());

        maxHealth *= manager.enemyScaling;
        xpGiveAmmount *= manager.enemyScaling;

        if (GetComponent<shooting>() != null)
        {
            GetComponent<shooting>().attackDamage = (int)Mathf.Round(GetComponent<shooting>().attackDamage * manager.enemyScaling);
        }
        else if (GetComponent<MeleeEnemy>() != null)
        {
            GetComponent<MeleeEnemy>().damageAmount = (int)Mathf.Round(GetComponent<MeleeEnemy>().damageAmount * manager.enemyScaling);
        }

        health = maxHealth;
    }

    [SerializeField] private float iFramesInSeconds = 0.1f;
    private bool isIFrameActive = false;

    public UnityEvent OnDeath { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public UnityEvent OnDamageTaken { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float IFramesInSeconds { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool IsIFrameActive { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void  ChangeHealth(float ammount)
    {
        if (ammount < 0)
        {
            if (!isIFrameActive)
            {
                health += ammount;
                isIFrameActive = true;
                Invoke(nameof(ResetIFrames), iFramesInSeconds);
            }
        }
        else
        {
            health += ammount;
        }
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0 && isAlive)
        {
            isAlive = false;

            GameObject.FindWithTag("Player").GetComponent<XpSystem>().GiveXP(xpGiveAmmount, 2.5f);

            onKilled.Invoke();
            Destroy(gameObject);
        }
    }

    private void ResetIFrames()
    {
        isIFrameActive = false;
        CancelInvoke(nameof(ResetIFrames));
    }

    public void RemoveIFrames()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeHealth(float ammount, bool ignoreIframes)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
