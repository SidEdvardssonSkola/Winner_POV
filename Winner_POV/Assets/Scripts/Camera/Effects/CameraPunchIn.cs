using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPunchIn : MonoBehaviour
{
    private Camera camera;
    float oldSize;
    private void Start()
    {
        camera = GetComponent<Camera>();
        oldSize = camera.orthographicSize;
    }

    public void StartEffect(float intensity, float resizeDurationInSeconds)
    {
        intensity = Mathf.Clamp(intensity, 0.01f, 0.99f);
        intensity = 1 - intensity;

        camera.orthographicSize = oldSize * intensity;

        StopAllCoroutines();
        StartCoroutine(ReSizeCamera(resizeDurationInSeconds, oldSize));
    }

    private IEnumerator ReSizeCamera(float duration, float oldSize)
    {
        float ammountToResize = (oldSize - camera.orthographicSize) / duration;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            camera.orthographicSize += ammountToResize * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        camera.orthographicSize = oldSize;
    }
}
