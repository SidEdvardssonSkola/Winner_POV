using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    [SerializeField] private GameObject[] attackHurtBoxes;
    public void LoadVictoyScreen()
    {
        SceneManager.LoadScene(2);
    }

    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearSpawnPos;
    public void SpawnSpear()
    {
        Instantiate(spearPrefab, spearSpawnPos.position, spearPrefab.transform.rotation);
    }
}
