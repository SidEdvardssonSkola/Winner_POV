using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Shoot Projectile at Player", menuName = "Enemy/Behaviours/Attack States/Shoot Projectile at Player")]
public class ShootProjectile : AttackBase
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float shootCooldown = 5.5f;
    private float timer = 0;

    [SerializeField] private float floatiness = 0.5f;

    private Transform projectileSpawnPos;

    private Animator animator;
    private bool hasTrigger = false;
    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);

        if (projectile == null)
        {
            Debug.Log("Warning! No Projectile Prefab Found at " +  gameObject.name + "!");
        }

        foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Projectile Spawn Pos"))
            {
                projectileSpawnPos = t;
                break;
            }
        }

        if (projectileSpawnPos == null)
        {
            Debug.Log("Warning! No Projectile Spawn Position Found at " + gameObject.name + "!");
            projectileSpawnPos = gameObject.transform;
        }

        animator = gameObject.GetComponent<Animator>();

        hasTrigger = false;
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            if (animator.parameters[i].name == "Shoot")
            {
                hasTrigger = true;
                enemy.isAnimationFinished.Add("Shoot", false);
                break;
            }
        }

        timer = Random.Range(0, shootCooldown / 1.5f);
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
            try
            {
                if (hasTrigger)
                {
                    animator.SetTrigger("Shoot");
                    enemy.StartCoroutine(WaitUntilAnimationIsDone());
                }
                else
                {
                    SpawnProjectile();
                }
            }
            catch
            {
                SpawnProjectile();
            }
        }
    }

    private IEnumerator WaitUntilAnimationIsDone()
    {
        while (!enemy.isAnimationFinished["Shoot"])
        {
            yield return null;
        }
        enemy.isAnimationFinished["Shoot"] = false;

        SpawnProjectile();
    }

    public void SpawnProjectile()
    {
        Vector2 direction = new(playerTransform.position.x - projectileSpawnPos.position.x, playerTransform.position.y - projectileSpawnPos.position.y);
        direction = direction.normalized;

        Projectile p = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity).GetComponent<Projectile>();
        p.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
        p.Init(direction, damage);
    }
    
}
