using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    [SerializeField] private XpSystem xpManager;

    [SerializeField] private float maxSize = 6;
    private Vector3 scale;

    private void Start()
    {
        scale = new(transform.localScale.x, 0, transform.localScale.z);
        transform.localScale = scale;
    }

    private void Awake()
    {
        xpManager.onXpChange.AddListener(UpdateXPBar);
    }

    public void UpdateXPBar()
    {
        scale.y = xpManager.xp * maxSize / xpManager.xpThreshold;
        transform.localScale = scale;
    }

    private void OnDisable()
    {
        xpManager.onXpChange.RemoveListener(UpdateXPBar);
    }
}
