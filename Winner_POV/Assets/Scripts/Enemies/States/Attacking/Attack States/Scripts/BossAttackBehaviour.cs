using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss Attack Behaviour", menuName = "Enemy/Behaviours/Attack States/Boss Behaviour")]
public class BossAttackBehaviour : AttackBase
{
    private Animator animator;
    [SerializeField] private string[] attacks;
    [SerializeField] private int avaliableAttacks = 2;

    [SerializeField] private float abilityCooldown = 6.5f;
    private float abilityCooldownTimer;

    [SerializeField] private float radius = 1;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < attacks.Length; i++)
        {
            enemy.isAnimationFinished.Add(attacks[i], false);
        }

        abilityCooldownTimer = abilityCooldown;

        enemy.OnDamageTaken.AddListener(CheckifShouldChangePhases);
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    bool canAttack = true;
    bool inMeleeRange = false;
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        abilityCooldownTimer -= Time.deltaTime;

        if (!canAttack) return;

        enemy.chaseState.FrameUpdate();

        inMeleeRange = false;
        foreach (Collider2D c in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (c.CompareTag("Player")) inMeleeRange = true;
        }

        if (abilityCooldownTimer <= 0 && canAttack)
        {
            abilityCooldownTimer = abilityCooldown;
            enemy.StartCoroutine(Attack(attacks[Random.Range(1, avaliableAttacks)]));
        }

        if (inMeleeRange && canAttack) enemy.StartCoroutine(Attack(attacks[0]));
    }

    private float safetyTimer;
    private IEnumerator Attack(string attackTrigger)
    {
        canAttack = false;
        safetyTimer = 10;

        animator.SetTrigger(attackTrigger);

        while (!enemy.isAnimationFinished[attackTrigger])
        {
            safetyTimer -= Time.deltaTime;
            if (safetyTimer <= 0)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        animator.SetTrigger("Walk");
        enemy.isAnimationFinished[attackTrigger] = false;
        canAttack = true;
    }

    [SerializeField] private float[] phaseThresholdInPercent;
    private int phase = 0;
    public void CheckifShouldChangePhases()
    {
        if (enemy.Health / enemy.MaxHealth < phaseThresholdInPercent[phase])
        {
            phase++;
            ChangePhase(phase);
        } 
    }

    private void ChangePhase(int phase)
    {
        switch (phase)
        {
            case 1:
                abilityCooldown *= 0.85f;
                avaliableAttacks = 3;
                break;

            case 2:
                abilityCooldown *= 0.85f;
                avaliableAttacks = 4;
                break;

            case 3:
                abilityCooldown *= 0.7f;
                break;
        }
    }
}
