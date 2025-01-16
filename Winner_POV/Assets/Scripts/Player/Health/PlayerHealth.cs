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

    private void Start()
    {
        maxHealth = startingMaxHealth;
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

    public void ChangeHealth(float ammount)
    {
        float oldHealth = health;
        health = Mathf.Clamp(health + ammount, 0, maxHealth);

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

        OnHealthChange.Invoke();
    }
}
