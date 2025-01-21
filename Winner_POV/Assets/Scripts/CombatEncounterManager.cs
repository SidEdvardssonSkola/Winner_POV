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

    public void AddEnemyToCounter(IDamageable enemy)
    {
        enemy.OnDeath.AddListener(CheckIfEncounterIsOver);
    }

    public void CheckIfEncounterIsOver()
    {
        CancelInvoke(nameof(Check));
        Invoke(nameof(Check), 0.5f);
    }

    private void Check()
    {
        if (GameObject.FindWithTag("Enemy") == null)
        {
            environment.SetActive(false);
            map.SetActive(true);

            Destroy(currentEncounter);

            EndEncounter();
        }
    }

    private void EndEncounter()
    {
        environment.SetActive(false);
        map.SetActive(true);

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(o);
        }

        Destroy(currentEncounter);
    }
}
