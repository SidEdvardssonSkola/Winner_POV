using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private HurtEnemy attack;

    public int Strength = 1;
    public int Vitality = 1;

    public UnityEvent onStrengthChange;
    public UnityEvent onVitalityChange;

    public void LevelUpStrength(int levels)
    {
        Strength += levels;
        attack.UpgradeDamage(Strength);
        onStrengthChange.Invoke();
    }

    public void LevelUpVitality(int levels)
    {
        Vitality += levels;
        playerHealth.ChangeMaxHealth();
        onVitalityChange.Invoke();
    }
}
