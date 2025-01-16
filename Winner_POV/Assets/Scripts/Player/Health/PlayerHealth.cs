using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private LevelUpSystem levelManager;

    public float startingMaxHealth = 100;
    public float maxHealth;
    public float health = 100;

    [SerializeField] private float maxHealthScaling = 1.25f;

    public UnityEvent OnHealthChange;

    [SerializeField] private ParticleSystem hitEffect;

    private void Start()
    {
        maxHealth = startingMaxHealth;
    }

    private void Awake()
    {
        isIframeActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeHealth(10);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeHealth(-10);
        }
    }

    public void ChangeMaxHealth()
    {
        float healthPercentage = health / maxHealth;
        maxHealth = startingMaxHealth * Mathf.Pow(maxHealthScaling, levelManager.Vitality - 1);
        health = maxHealth * healthPercentage;

        OnHealthChange.Invoke();
    }

    [SerializeField] private float iframesInSeconds = 0.1f;
    private bool isIframeActive = false;
    public void ChangeHealth(float ammount)
    {
        float oldHealth = health;

        if (ammount < 0 && !isIframeActive)
        {
            isIframeActive = true;
            Invoke(nameof(ResetIframes), iframesInSeconds);
            health = Mathf.Clamp(health + ammount, 0, maxHealth);

            hitEffect.Play();
        }
        else if (ammount > 0)
        {
            health = Mathf.Clamp(health + ammount, 0, maxHealth);
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

        OnHealthChange.Invoke();
    }

    public void ChangeHealth(float ammount, bool ignoreIframes)
    {
        float oldHealth = health;

        if (ammount < 0)
        {
            Invoke(nameof(ResetIframes), iframesInSeconds);
            health = Mathf.Clamp(health + ammount, 0, maxHealth);

            hitEffect.Play();
        }
        else if (ammount > 0)
        {
            health = Mathf.Clamp(health + ammount, 0, maxHealth);
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

        OnHealthChange.Invoke();
    }

    public void ResetIframes()
    {
        isIframeActive = false;
    }
}
