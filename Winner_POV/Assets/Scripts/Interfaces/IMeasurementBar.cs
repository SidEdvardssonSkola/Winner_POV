using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeasurementBar
{
    float MaxSize { get; set; }
    Vector3 scale { get; set; }

    void UpdateBar(float fillPercent);
}
