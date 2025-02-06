using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss Attack Behaviour", menuName = "Enemy/Behaviours/Attack States/Boss Behaviour")]
public class BossAttackBehaviour : AttackBase
{
    private Animator animator;
    [SerializeField] private string[] attacks;
    [SerializeField] private int avaliableAttacks = 2;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < attacks.Length; i++)
        {
            enemy.isAnimationFinished.Add(attacks[i], false);
        }


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
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Input.GetKeyDown(KeyCode.C))
        {
            enemy.StartCoroutine(Attack(attacks[Random.Range(0, avaliableAttacks)]));
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
            safetyTimer += Time.deltaTime;
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
