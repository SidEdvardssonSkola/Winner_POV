using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrengthText : MonoBehaviour
{
    [SerializeField] private LevelUpSystem levelManager;
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelManager.onStrengthChange.AddListener(UpdateText);;
    }

    public void UpdateText()
    {
        text.text = "Strength: " + levelManager.Strength;
    }

    private void OnDisable()
    {
        levelManager.onStrengthChange.RemoveListener(UpdateText);
    }
}
