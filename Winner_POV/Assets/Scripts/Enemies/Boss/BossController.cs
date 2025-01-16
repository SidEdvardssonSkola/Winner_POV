using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] bool isPlayerInRange = false;

    [SerializeField] private Transform particles;

    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float xScale;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.Play("BossWalk");
        StartCoroutine(ChasePlayer());
    }

    public void UpdateIsPlayerInRange(bool newIsPlayerInRange)
    {
        isPlayerInRange = newIsPlayerInRange;
    }

    private int direction;
    private IEnumerator ChasePlayer()
    {
        while (!isPlayerInRange)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                particles.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                direction = 1;
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                particles.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                direction = -1;
            }
            rb.velocity = new(speed * direction, rb.velocity.y);
            yield return null;
        }
        StartCoroutine(Attack());
    }

    [SerializeField] private float attackDuration;
    [SerializeField] private int attacksBeforeRoar;
    private int attackCounter;
    private IEnumerator Attack()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            direction = 1;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            direction = -1;
        }

        if (attackCounter >= attacksBeforeRoar)
        {
            attackCounter -= attacksBeforeRoar;
            StartCoroutine(Roar());
            yield return null;
        }
        else
        {
            attackCounter++;
            animator.Play("BossAttack_0");

            yield return new WaitForSeconds(attackDuration / 2);

            rb.velocity = new(speed * direction * 5f, rb.velocity.y);

            yield return new WaitForSeconds(attackDuration / 2);

            animator.Play("BossWalk");
            StartCoroutine(ChasePlayer());
        }
    }

    [SerializeField] private float roarDuration;
    private IEnumerator Roar()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            particles.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
        else
        {
            particles.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }

        animator.Play("BossRoar");

        yield return new WaitForSeconds(roarDuration);

        animator.Play("BossWalk");
        StartCoroutine(ChasePlayer());
    }
}
