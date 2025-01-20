using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBase : ScriptableObject
{
    protected GameObject gameObject;
    protected Transform transform;
    protected Enemy enemy;

    protected Transform playerTransform;

    public virtual void Init(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void OnStateUpdate() { }
}
