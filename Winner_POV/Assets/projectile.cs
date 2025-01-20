using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour, IDamageable
{
    public float projectileSpeed = 10f; // Speed of the projectile
    public float lifetime = 5f; // Time before the projectile disappears

    private Rigidbody2D rb;

    public UnityEvent OnDeath { get; set;  }
    public UnityEvent OnDamageTaken { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; } = 25f;
    public float Health { get; set; }
    [field: SerializeField] public float IFramesInSeconds { get; set; } = 0.25f;
    public bool IsIFrameActive { get; set; } = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        Destroy(gameObject, lifetime); // Destroy the projectile after a set time
    }

    public void Init(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the projectile if it hits the player
        }
        else if (!collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy projectile if it hits a wall
        }
    }

    #region Health
    public void ChangeHealth(float ammount)
    {
        Health += ammount;

        if (ammount < 0 && !IsIFrameActive)
        {
            IsIFrameActive = true;
            Invoke(nameof(RemoveIFrames), IFramesInSeconds);
            OnDamageTaken.Invoke();

            if (Health <= 0)
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
}
