using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    [SerializeField] private GameObject[] attackHurtBoxes;
    private Transform playerTransform;

    [SerializeField] private float dashSpeedMultiplier = 5.56f;

    public override void Start()
    {
        base.Start();

        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void LoadVictoyScreen()
    {
        SceneManager.LoadScene(2);
    }

    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearSpawnPos;
    public void SpawnSpear()
    {
        Projectile s = Instantiate(spearPrefab, spearSpawnPos.position, spearPrefab.transform.rotation).GetComponentInChildren<Projectile>();
        s.Init(playerTransform.position - transform.position, attackBaseReference.damage);
    }

    [SerializeField] private Transform forward;
    public void DashForward()
    {
        Debug.Log("Dashning");
        AddVelocity((forward.position - transform.position).normalized * Speed * dashSpeedMultiplier);
    }
}
