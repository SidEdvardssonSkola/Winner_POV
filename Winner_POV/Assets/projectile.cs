using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour, IDamageable
{
    public float projectileSpeed = 10f; // Speed of the projectile
    public float lifetime = 5f; // Time before the projectile disappears

    private float damage = 0;

    private Rigidbody2D rb;

    [field: SerializeField] public UnityEvent OnDeath { get; set;  }
    [field: SerializeField] public UnityEvent OnDamageTaken { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; } = 25f;
    public float Health { get; set; }
    [field: SerializeField] public float IFramesInSeconds { get; set; } = 0.25f;
    public bool IsIFrameActive { get; set; } = false;

    [SerializeField] private GameObject particle;

    [SerializeField] private AudioSource spawnSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        Destroy(gameObject, lifetime); // Destroy the projectile after a set time

        if (spawnSound != null)
        {
            spawnSound.pitch = Random.Range(0.75f, 1.25f);
            spawnSound.Play();
        }
    }

    public void Init(Vector2 direction, float damage)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));

        this.damage = damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            Destroy(gameObject); // Destroy the projectile if it hits the player
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        { 
            
            if (particle != null) Instantiate(particle, transform.position, Quaternion.identity);
        
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

            if (particle != null) Instantiate(particle, transform.position, Quaternion.identity);

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

            Instantiate(particle, transform.position, Quaternion.identity);

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
