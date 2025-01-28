using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.idleBaseReference.OnStateEnter();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.idleBaseReference.OnStateExit();
    }

    public override void FrameUpdate()
    {
        if (enemy.isDead)
        {
            return;
        }
        base.FrameUpdate();
        enemy.idleBaseReference.OnStateUpdate();
    }
}
