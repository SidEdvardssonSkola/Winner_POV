using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable, IBasicMovement
{
    #region Start Functions
    private void Awake()
    {
        idleBaseReference = Instantiate(idleBase);
        chaseBaseReference = Instantiate(chaseBase);
        attackBaseReference = Instantiate(attackBase);

        enemyStateMachine = new();

        idleState = new(this, enemyStateMachine);
        chaseState = new(this, enemyStateMachine);
        attackState = new(this, enemyStateMachine);
    }

    public virtual void Start()
    {
        Health = MaxHealth;
        Rb = gameObject.GetComponent<Rigidbody2D>();

        idleBaseReference.Init(gameObject, this);
        chaseBaseReference.Init(gameObject, this);
        attackBaseReference.Init(gameObject, this);

        enemyStateMachine.Init(idleState);
    }
    #endregion

    #region Health

    [field: Header("Health")]

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float Health { get; set; }

    [field: SerializeField] public float IFramesInSeconds { get; set; } = 0.25f;
    public bool IsIFrameActive { get; set; }

    [field: SerializeField] public UnityEvent OnDeath { get; set; }
    [field: SerializeField] public UnityEvent OnDamageTaken { get; set; }

    public void ChangeHealth(float ammount)
    {
        Health += ammount;

        if (ammount < 0 && !IsIFrameActive)
        {
            IsIFrameActive = true;
            Invoke(nameof(RemoveIFrames), IFramesInSeconds);
            OnDamageTaken.Invoke();

            if (Health <= 0 )
            {
                Die();
            }
        }
    }

    public void RemoveIFrames()
    {
        IsIFrameActive = false;
        CancelInvoke(nameof(RemoveIFrames));
    }
    
    public void ChangeHealth(float ammount, bool ignoreIframes)
    {
        Health += ammount;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        if (ammount < 0)
        {
            OnDamageTaken.Invoke();

            if (Health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
    #endregion

    #region Movement

    [field: Header("Movement")]

    public Rigidbody2D Rb { get; set; }

    [field: SerializeField] public float Speed { get; set; } = 5f;
    [field: SerializeField] public float Acceleration { get; set; } = 3f;

    [field: SerializeField] public bool IsFacingRight { get; set; } = true;

    public void AddVelocity(Vector2 velocityToAdd)
    {
        Rb.AddForce(velocityToAdd);
        CheckIfFacingLeftOrRight(velocityToAdd.x);
        StartCoroutine(DampVelocityToCap());
    }

    public void CheckIfFacingLeftOrRight(float xVelocity)
    {
        if (xVelocity > 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180 * (IsFacingRight ? 0 : 1), transform.eulerAngles.z);
        }
        else if (xVelocity < 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180 * (IsFacingRight ? 1 : 0), transform.eulerAngles.z);
        }
    }

    public IEnumerator DampVelocityToCap()
    {
        bool shouldLoop = true;
        while (shouldLoop)
        {
            shouldLoop = false;
            if (Mathf.Abs(Rb.velocity.x) > Speed)
            {
                Rb.AddForce(new Vector2(Acceleration * Mathf.Round(Mathf.Clamp(-Rb.velocity.x, -1, 1)), 0));
                shouldLoop = true;
            }
            if (Mathf.Abs(Rb.velocity.y) > Speed)
            {
                Rb.AddForce(new Vector2(0, Acceleration * Mathf.Round(Mathf.Clamp(-Rb.velocity.y, -1, 1))));
                shouldLoop = true;
            }
            yield return null;
        }
    }
    #endregion

    #region State Machine

    public EnemyStateMachine enemyStateMachine { get; set; }

    public IdleState idleState { get; set; }
    public ChaseState chaseState { get; set; }
    public AttackState attackState { get; set; }

    [SerializeField] private IdleBase idleBase;
    [SerializeField] private ChaseBase chaseBase;
    [SerializeField] private AttackBase attackBase;

    public IdleBase idleBaseReference { get; set; }
    public ChaseBase chaseBaseReference { get; set; }
    public AttackBase attackBaseReference { get; set; }

    public void SetStatus(int newStatus)
    {
        switch (newStatus)
        {
            case 0:
                enemyStateMachine.ChangeState(idleState);
                break;

            case 1:
                enemyStateMachine.ChangeState(chaseState);
                break;

            case 2:
                enemyStateMachine.ChangeState(attackState);
                break;
        }
    }

    #endregion

    private void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();
    }
}
