using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

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
        GameObject encounterManager = GameObject.FindWithTag("Encounter Manager");
        if (encounterManager != null)
        {
            MaxHealth *= Mathf.Pow(healthIncreasePerLevelInPercent, encounterManager.GetComponent<CombatEncounterManager>().enemyScaling);
            xpGiveAmmount *= Mathf.Pow(healthIncreasePerLevelInPercent, encounterManager.GetComponent<CombatEncounterManager>().enemyScaling);
        }

        Health = MaxHealth;
        Rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        idleBaseReference.Init(gameObject, this);
        chaseBaseReference.Init(gameObject, this);
        attackBaseReference.Init(gameObject, this);

        enemyStateMachine.Init(idleState);

        if (GameObject.FindWithTag("Encounter Manager") != null)
        {
            GameObject.FindWithTag("Encounter Manager").GetComponent<CombatEncounterManager>().AddEnemyToCounter(this);
        }

        healthBar = Instantiate(healthBarGameObject, transform.position + healthBarOffset, Quaternion.identity, transform).GetComponent<MeasurementBar>();
        if (healthBar != null)
        {
            healthBar.gameObject.transform.localScale = new(2 / transform.localScale.x, 0.3f / transform.localScale.y, 1);
            OnDamageTaken.AddListener(() => healthBar.UpdateBar(Health / MaxHealth));
        }

        cameraPunch = GameObject.FindWithTag("MainCamera").GetComponent<CameraPunchIn>();
    }
    #endregion

    #region Health

    [field: Header("Health")]

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [SerializeField] private float healthIncreasePerLevelInPercent = 1.12f;
    public float Health { get; set; }

    [field: SerializeField] public float IFramesInSeconds { get; set; } = 0.25f;
    public bool IsIFrameActive { get; set; }

    [field: SerializeField] public UnityEvent OnDeath { get; set; }
    [field: SerializeField] public UnityEvent OnDamageTaken { get; set; }

    [SerializeField] private GameObject healthBarGameObject;
    [SerializeField] private Vector3 healthBarOffset = new(0, 2.5f, 0);
    private MeasurementBar healthBar;

    [SerializeField] private float xpGiveAmmount = 50f;

    [SerializeField] private GameObject damagePopup;
    [SerializeField] private Vector2 minPopupOffset = new(-1, 1);
    [SerializeField] private Vector2 maxPopupOffset = new(1, 2);

    private CameraPunchIn cameraPunch;

    public void ChangeHealth(float ammount)
    {
        Health += ammount;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        if (ammount < 0 && !IsIFrameActive)
        {
            IsIFrameActive = true;
            Invoke(nameof(RemoveIFrames), IFramesInSeconds);
            OnDamageTaken.Invoke();

            if (damagePopup != null)
            {
                CreateDamagePopup((int)Mathf.Round(ammount));
            }

            if (Health <= 0 )
            {
                Die();
            }
        }
    }

    private void CreateDamagePopup(int damage)
    {
        Vector3 offset = new(UnityEngine.Random.Range(minPopupOffset.x, maxPopupOffset.x), UnityEngine.Random.Range(minPopupOffset.y, maxPopupOffset.y), -1);
        DamagePopup popup = Instantiate(damagePopup, transform.position + offset, Quaternion.identity).GetComponent<DamagePopup>();
        popup.SetText(damage);
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

    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public void Flash()
    {
        spriteRenderer.color = Color.red;

        Invoke(nameof(UnFlash), IFramesInSeconds);
    }
    private void UnFlash()
    {
        CancelInvoke(nameof(UnFlash));
        spriteRenderer.color = originalColor;
    }

    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isAnimationDone = false;
    public void Die()
    {
        if (!isDead)
        {
            GameObject.FindWithTag("Player").GetComponent<XpSystem>().GiveXP(xpGiveAmmount, 2.5f);

            isDead = true;
            cameraPunch.StartEffect(0.1f, 0.25f);

            StartCoroutine(FinishDeath());
        }
    }

    private IEnumerator FinishDeath()
    {
        try
        {
            Animator animator = GetComponent<Animator>();
            bool triggerExists = false;

            for (int i = 0; i < animator.parameters.Length; i++)
            {
                if (animator.parameters[i].name == "Die")
                {
                    triggerExists = true;
                    break;
                }
            }

            if (triggerExists)
            {
                animator.SetTrigger("Die");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        catch
        {
            Destroy(gameObject);
        }

        float safetyTimer = 7.5f;
        while (!isAnimationDone)
        {
            safetyTimer -= Time.deltaTime;

            if (safetyTimer <= 0)
            {
                break;
            }

            yield return null;
        }
        isAnimationDone = false;

        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void SetAnimationDone()
    {
        isAnimationDone = true;
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

    bool isAggroed = false;
    bool isAttacking = false;

    public void SetStatus(int status, bool isActive)
    {
        switch (status)
        {
            case 0:
                enemyStateMachine.ChangeState(idleState);
                break;

            case 1:
                isAggroed = isActive;
                enemyStateMachine.ChangeState(chaseState);
                break;

            case 2:
                enemyStateMachine.ChangeState(attackState);
                isAttacking = isActive;
                break;
        }
    }

    #endregion

    private void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();
    }
}
