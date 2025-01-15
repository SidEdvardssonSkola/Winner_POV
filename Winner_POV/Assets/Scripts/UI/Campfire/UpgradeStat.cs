using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStat : MonoBehaviour
{
    [SerializeField] private LevelUpSystem levelUpSystem;
    [SerializeField] private XpSystem xpManager;
    public void Upgrade(int statType)
    {
        if (xpManager.levelUpPoints > 0)
        {
            switch (statType)
            {
                case 0:
                    //vitality
                    xpManager.levelUpPoints--;
                    levelUpSystem.LevelUpVitality(1);
                    break;

                case 1:
                    //strength
                    xpManager.levelUpPoints--;
                    levelUpSystem.LevelUpStrength(1);
                    break;
            }
        }
    }
}
