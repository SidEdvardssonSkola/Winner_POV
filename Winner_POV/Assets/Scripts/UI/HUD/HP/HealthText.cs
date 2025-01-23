using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    TextMeshProUGUI text;
    PlayerHealth playerhealth;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        playerhealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerhealth.OnHealthChange.AddListener(() => UpdateHealthText(playerhealth.health, playerhealth.maxHealth));
    }
    public void UpdateHealthText(float health, float maxhealth)
    {
        text.text = "Health: " + Mathf.Round(health) + " (" + Mathf.Round(health / maxhealth * 100) + "%)";
    }
}
