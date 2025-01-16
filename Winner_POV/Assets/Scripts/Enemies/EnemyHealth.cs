using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    public UnityEvent onKilled;

    void Start()
    {
        health = maxHealth;
        GameObject.Find("Encounter Manager").GetComponent<CombatEncounterManager>().AddEnemyToCounter(GetComponent<EnemyHealth>());
    }
}
