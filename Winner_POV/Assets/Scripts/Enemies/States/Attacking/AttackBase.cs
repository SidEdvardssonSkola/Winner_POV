using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : ScriptableObject
{
    protected GameObject gameObject;
    protected Transform transform;
    protected Enemy enemy;

    protected Transform playerTransform;

    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float damageIncreasePerLevelInPercent = 1.12f;

    public virtual void Init(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindWithTag("Player").transform;

        GameObject encounterManager = GameObject.FindWithTag("Encounter Manager");
        if (encounterManager != null)
        {
            damage *= Mathf.Pow(damageIncreasePerLevelInPercent, encounterManager.GetComponent<CombatEncounterManager>().enemyScaling);
        }
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void OnStateUpdate() { }
}
