using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEncounterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] combatEncounters;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject environment;

    private GameObject currentEncounter;
    public void SpawnRandomEncounter()
    {
        int randomNumber = Random.Range(0, combatEncounters.Length);
        currentEncounter = Instantiate(combatEncounters[randomNumber]);
    }

    private int remainingEnemies = 0;
    public void AddEnemyToCounter(EnemyHealth enemy)
    {
        remainingEnemies++;
        print(remainingEnemies);

        enemy.onKilled.AddListener(RemoveEnemyFromCounter);
    }

    public void RemoveEnemyFromCounter()
    {
        remainingEnemies--;
        print(remainingEnemies);
        if (remainingEnemies <= 0)
        {
            map.SetActive(true); 
            environment.SetActive(false);

            Destroy(currentEncounter);
        }
    }
}
