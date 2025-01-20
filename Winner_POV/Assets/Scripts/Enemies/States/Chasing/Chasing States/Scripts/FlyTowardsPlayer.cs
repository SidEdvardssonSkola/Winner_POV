using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Fly Towards Player", menuName = "Enemy/Behaviours/Chase States/Fly Towards Player")]
public class FlyTowardsPlayer : ChaseBase
{
    [SerializeField] private float verticalSpeedMultiplier = 1f;
    [SerializeField] private float horizontalSpeedMultiplier = 1f;
    private Vector2 direction;
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

        direction = playerTransform.position - transform.position;
        direction = direction.normalized;
        enemy.AddVelocity(new Vector2(direction.x * horizontalSpeedMultiplier * enemy.Acceleration, direction.y * verticalSpeedMultiplier * enemy.Acceleration));
    }
}
