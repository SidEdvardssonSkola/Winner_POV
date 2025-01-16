using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEncounter : MonoBehaviour
{
    private float scaling = 1;
    public void SetScaling(float newScaling)
    {
        scaling = newScaling;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PrepareEncounter);
    }

    public void PrepareEncounter()
    {
        GameObject.Find("Encounter Manager").GetComponent<CombatEncounterManager>().SpawnRandomEncounter();
    }
}
