using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementBar : MonoBehaviour, IMeasurementBar
{
    [SerializeField] private Transform bar;
    [SerializeField] private bool hideUntilFirstUpdate = true;
    public float MaxSize { get; set; } = 1f;
    public Vector3 scale { get; set; }

    private void Start()
    {
        if (hideUntilFirstUpdate)
        {
            transform.Rotate(0, 90, 0);
        }
    }
    public void UpdateBar(float fillPercent)
    {
        transform.eulerAngles = Vector3.zero;

        scale = new(MaxSize * fillPercent, bar.localScale.y, bar.localScale.z);
        bar.localScale = scale;
    }
}
