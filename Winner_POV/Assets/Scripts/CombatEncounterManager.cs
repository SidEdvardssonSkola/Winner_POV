using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEncounterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] combatEncounters;
    [SerializeField] private GameObject[] eliteCombatEncounters;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject environment;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 playerSpawnPos;

    [SerializeField] private Animator transition;

    private bool isTransitionDone = false;

    private GameObject currentEncounter;
    public float enemyScaling;

    public void SpawnRandomEncounter(int scaling)
    {
        enemyScaling = scaling;

        isTransitionDone = false;
        transition.SetTrigger("Play Transition");
        StartCoroutine(FinishEncounterSpawn());
    }
    private IEnumerator FinishEncounterSpawn()
    {
        while (isTransitionDone == false)
        {
            yield return new WaitForEndOfFrame();
        }
        isTransitionDone = false;

        player.position = playerSpawnPos;

        map.SetActive(false);

        int randomNumber = Random.Range(0, combatEncounters.Length);
        currentEncounter = Instantiate(combatEncounters[randomNumber]);
    }

    public void SpawnRandomEliteEncounter(int scaling)
    {
        enemyScaling = scaling;

        isTransitionDone = false;
        transition.SetTrigger("Play Transition");
        StartCoroutine(FinishEliteEncounterSpawn());
    }
    private IEnumerator FinishEliteEncounterSpawn()
    {
        while (isTransitionDone == false)
        {
            yield return new WaitForEndOfFrame();
        }
        isTransitionDone = false;

        player.position = playerSpawnPos;

        map.SetActive(false);

        int randomNumber = Random.Range(0, eliteCombatEncounters.Length);
        currentEncounter = Instantiate(eliteCombatEncounters[randomNumber]);
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
            EndEncounter();
        }
    }

    private void EndEncounter()
    {
        isTransitionDone = false;
        transition.SetTrigger("Play Transition");

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(o);
        }

        StartCoroutine(FinishEncounterEnd());
    }
    private IEnumerator FinishEncounterEnd()
    {
        while (isTransitionDone == false)
        {
            yield return new WaitForEndOfFrame();
        }
        isTransitionDone = false;

        environment.SetActive(false);
        map.SetActive(true);

        Destroy(currentEncounter);
    }

    public void SetTransitionDone()
    {
        isTransitionDone = true;
    }
}
