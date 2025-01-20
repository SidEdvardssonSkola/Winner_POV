using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Horizontal Only", menuName = "Enemy/Behaviours/Chase States/Chase Player Horizontally")]
public class MoveHorizontallyToPlayer : ChaseBase
{
    [SerializeField] private float speedMultiplier = 1f;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
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
    }
}
