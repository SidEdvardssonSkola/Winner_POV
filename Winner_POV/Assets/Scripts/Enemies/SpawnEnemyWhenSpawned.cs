using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyWhenSpawned : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
    void Start()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
