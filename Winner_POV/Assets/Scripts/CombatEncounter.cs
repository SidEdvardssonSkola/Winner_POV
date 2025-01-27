using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEncounter : MonoBehaviour
{
    int scaling;
    bool isElite;
    public void Init(int _scaling, bool _isElite)
    {
        scaling = _scaling;
        isElite = _isElite;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PrepareEncounter);
    }

    public void PrepareEncounter()
    {
        if (isElite)
        {
            GameObject.Find("Encounter Manager").GetComponent<CombatEncounterManager>().SpawnRandomEliteEncounter(scaling);
        }
        else
        {
            GameObject.Find("Encounter Manager").GetComponent<CombatEncounterManager>().SpawnRandomEncounter(scaling);
        }
    }
}
