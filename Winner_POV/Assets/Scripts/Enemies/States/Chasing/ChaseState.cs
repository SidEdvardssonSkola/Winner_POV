using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.chaseBaseReference.OnStateEnter();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.chaseBaseReference.OnStateExit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.chaseBaseReference.OnStateUpdate();
    }
}
