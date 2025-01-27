using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-HP Drain", menuName = "Enemy/Behaviours/Attack States/Drain Player Health")]
public class DrainPlayerHP : AttackBase
{
    private PlayerHealth playerHealth;

    private float timer = 0;
    [SerializeField] private float appliesPerSecond = 5;
    private float damagePerApply;
    private float waitBetweenApplies;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        damagePerApply = damage / appliesPerSecond;
        waitBetweenApplies = 1 / appliesPerSecond;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        timer += Time.deltaTime;
        if (timer > waitBetweenApplies)
        {
            playerHealth.ChangeHealth(-damagePerApply);
            timer = 0;
        }
    }
}
