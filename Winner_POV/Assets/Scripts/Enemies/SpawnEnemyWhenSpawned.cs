using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyWhenSpawned : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    void Start()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
