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

        if (Physics2D.OverlapCircle(transform.position, radius).CompareTag("Player")) inMeleeRange = true;
        else inMeleeRange = false;

        if (inMeleeRange && canAttack) enemy.StartCoroutine(Attack(attacks[0]));

        if (abilityCooldownTimer <= 0 && canAttack)
        {
            abilityCooldownTimer = abilityCooldown;
            enemy.StartCoroutine(Attack(attacks[Random.Range(1, avaliableAttacks)]));
        }
    }

    private float safetyTimer;
    private IEnumerator Attack(string attackTrigger)
    {
        canAttack = false;
        safetyTimer = 10;
        animator.SetTrigger(attackTrigger);

        while (!enemy.isAnimationFinished[attackTrigger] && safetyTimer < 10)
        {
            safetyTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        canAttack = true;
    }

    public void CheckifShouldChangePhases()
    {

    }

    private void ChangePhase()
    {

    }
}
