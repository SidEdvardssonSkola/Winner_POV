using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{

    public AttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.attackBaseReference.OnStateEnter();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.attackBaseReference.OnStateExit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.attackBaseReference.OnStateUpdate();
    }
}
