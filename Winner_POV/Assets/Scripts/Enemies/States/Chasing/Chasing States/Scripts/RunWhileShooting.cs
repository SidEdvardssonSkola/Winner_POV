using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run at Player While Shooting", menuName = "Enemy/Behaviours/Chase States/Run at Player While Shooting")]
public class RunWhileShooting : ChaseBase
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float damage = 20;
    [SerializeField] private float shootCooldown = 5.5f;
    private float timer = 0;

    [SerializeField] private float speedMultiplier = 1f;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        if (projectile == null)
        {
            Debug.Log("Warning! No Projectile Prefab Found at " + gameObject.name + "!");
        }
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

        timer += Time.deltaTime;
        if (timer > shootCooldown)
        {
            timer = 0;

            Vector2 direction = new(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
            direction = direction.normalized;

            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
            p.Init(direction, damage);
        }
    }
}
