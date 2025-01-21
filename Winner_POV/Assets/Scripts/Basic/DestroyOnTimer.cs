using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField] private float timeUntilDestroy = 1.5f;
    void Start()
    {
        Invoke(nameof(DestroyGameObject), timeUntilDestroy);
    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
