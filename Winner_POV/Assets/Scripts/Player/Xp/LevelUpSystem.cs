using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpSystem : MonoBehaviour
{
    public int Strength = 1;
    public int Vitality = 1;

    public UnityEvent onStrengthChange;
    public UnityEvent onVitalityChange;

    public void LevelUpStrength(int levels)
    {
        Strength += levels;
        onStrengthChange.Invoke();
    }

    public void LevelUpVitality(int levels)
    {
        Vitality += levels;
        onVitalityChange.Invoke();
    }
}
