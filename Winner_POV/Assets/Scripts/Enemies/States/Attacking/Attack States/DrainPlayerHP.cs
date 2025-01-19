using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrainPlayerHP : AttackState
{
    private PlayerHealth player;

    public DrainPlayerHP(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.ChangeHealth(-10 * Time.deltaTime, true);
    }
}
