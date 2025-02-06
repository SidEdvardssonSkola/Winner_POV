using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run at Player While Shooting", menuName = "Enemy/Behaviours/Chase States/Run at Player While Shooting")]
public class RunWhileShooting : ChaseBase
{
    [SerializeField] private float speedMultiplier = 1f;
    private AttackBase shootScriptReference;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
        shootScriptReference = enemy.attackBaseReference;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (enemy.transform.position.x < playerTransform.position.x)
        {
            enemy.AddVelocity(new Vector2(enemy.Acceleration * speedMultiplier, 0));
        }
        else
        {
            enemy.AddVelocity(new Vector2(-enemy.Acceleration * speedMultiplier, 0));
        }

        shootScriptReference.OnStateUpdate();
    }
}
