using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 pos;
    private float elapsedTime = 0;
    private void Start()
    {
        pos = transform.position;
    }
    public void ShakeScreen(float duration, float intensity)
    {
        elapsedTime = 0;
        StartCoroutine(Shake(duration, intensity));
    }
    private IEnumerator Shake(float duration, float intensity)
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = pos + new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0);

            yield return null;
        }

        transform.position = pos;
    }
}
