using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
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
        manager.AddEnemyToCounter(GetComponent<EnemyHealth>());

        maxHealth *= manager.enemyScaling;
        xpGiveAmmount *= manager.enemyScaling;
        health = maxHealth;
    }

    [SerializeField] private float iFramesInSeconds = 0.1f;
    private bool isIFrameActive = false;
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

            GameObject.Find("Player (1)").GetComponent<XpSystem>().GiveXP(xpGiveAmmount, 2.5f);

            onKilled.Invoke();
            Destroy(gameObject);
        }
    }

    private void ResetIFrames()
    {
        isIFrameActive = false;
        CancelInvoke(nameof(ResetIFrames));
    }
}
