using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private float maxSize = 4.18f;
    private Vector3 scale;

    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        scale = transform.localScale;
        scale.x = maxSize;
        playerHealth.OnHealthChange.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar()
    {
        float oldSize = scale.x;
        scale = new(maxSize * (playerHealth.health / playerHealth.maxHealth), transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;

        if(oldSize > scale.x)
        {
            particle.Play();
        }
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChange.RemoveListener(UpdateHealthBar);
    }
}
