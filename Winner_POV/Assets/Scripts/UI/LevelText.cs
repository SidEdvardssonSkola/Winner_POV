using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] private XpSystem xpManager;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        xpManager.onLevelUp.AddListener(UpdateText);
    }

    public void UpdateText()
    {
        text.text = "Level: " + xpManager.level;
    }

    private void OnDisable()
    {
        xpManager.onLevelUp.RemoveListener(UpdateText);
    }
}
