using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{

    private PlayerHealth player;

    public AttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering attack");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting attack");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.ChangeHealth(-10 * Time.deltaTime, true);
    }
}
