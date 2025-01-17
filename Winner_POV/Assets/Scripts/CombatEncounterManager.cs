using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEncounterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] combatEncounters;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject environment;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 playerSpawnPos;

    private GameObject currentEncounter;
    public float enemyScaling;
    public void SpawnRandomEncounter(float scaling)
    {
        player.position = playerSpawnPos;

        enemyScaling = scaling;

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
            environment.SetActive(false);
            map.SetActive(true);

            Destroy(currentEncounter);
        }
    }
}
