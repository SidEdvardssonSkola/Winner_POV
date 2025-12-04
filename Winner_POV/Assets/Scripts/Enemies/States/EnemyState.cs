using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine)
    {
        enemy = _enemy;
        enemyStateMachine = _enemyStateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    //Den är inte frameupdate utan fixedupdate, jag orkar inte ändra namnet på den överallt så det får vara lite missledande
    public virtual void FrameUpdate()
    {

    }
}
