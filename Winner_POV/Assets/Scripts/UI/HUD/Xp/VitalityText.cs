using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VitalityText : MonoBehaviour
{
    [SerializeField] private LevelUpSystem levelManager;
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelManager.onVitalityChange.AddListener(UpdateText);
    }

    public void UpdateText()
    {
        text.text = "Vitality: " + levelManager.Vitality;
    }

    private void OnDisable()
    {
        levelManager.onVitalityChange.RemoveListener(UpdateText);
    }
}
