using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Shoot Projectile at Player", menuName = "Enemy/Behaviours/Attack States/Shoot Projectile at Player")]
public class ShootProjectile : AttackBase
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float shootCooldown = 5.5f;
    private float timer = 0;

    [SerializeField] private float floatiness = 0.5f;
    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        if (projectile == null)
        {
            Debug.Log("Warning! No Projectile Prefab Found at " +  gameObject.name + "!");
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

        enemy.AddVelocity(new Vector2(Random.Range(-floatiness, floatiness) * enemy.Acceleration, Random.Range(-floatiness, floatiness) * enemy.Acceleration));
        enemy.CheckIfFacingLeftOrRight(playerTransform.position.x - transform.position.x);

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
