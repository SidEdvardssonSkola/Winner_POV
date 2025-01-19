using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private Transform player;
    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering chase");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting chase");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.transform.position.x < player.position.x)
        {
            enemy.AddVelocity(new Vector2(enemy.Acceleration, 0));
        }
        else
        {
            enemy.AddVelocity(new Vector2(-enemy.Acceleration, 0));
        }
    }
}
