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
        Debug.Log("Entering idle");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting idle");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
