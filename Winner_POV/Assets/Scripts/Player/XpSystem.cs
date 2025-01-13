using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XpSystem : MonoBehaviour
{
    public int level = 1;
    private int levelUpPoints = 0;

    [Header("XP Threshold = Startvärdet för Antal XP som Krävs för att Levla Upp")]
    [SerializeField] public float xpThreshold = 100;
    [SerializeField] private float xpThresholdScaling = 1.2f;

    public float xp;

    public UnityEvent onXpChange;
    public UnityEvent onLevelUp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GiveXP(10);
        }
    }
    public void GiveXP(float ammountToGive)
    {
        xp += ammountToGive;

        while (xp >= xpThreshold)
        {
            level += 1;
            levelUpPoints += 1;

            xp -= xpThreshold;
            xpThreshold *= xpThresholdScaling;

            onLevelUp.Invoke();

            print("Level Up! New Level is " + level + "!");
        }

        onXpChange.Invoke();
    }
}
