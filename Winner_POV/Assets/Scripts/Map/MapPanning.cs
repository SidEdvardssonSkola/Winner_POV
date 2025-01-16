using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanning : MonoBehaviour
{
    [SerializeField] private float panSpeed = 5.5f;
    [SerializeField] private float panThreshold = 0.15f;

    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;
    private float yPos;
    private int panDirection = 0;
    private void Update()
    {
        if (Input.mousePosition.y > Screen.height * (1 - panThreshold))
        {
            panDirection = 1;
        }
        else if (Input.mousePosition.y < Screen.height * panThreshold)
        {
            panDirection = -1;
        }
        else
        {
            panDirection = 0;
        }
        yPos += panSpeed * -panDirection * Time.deltaTime;
        yPos = Mathf.Clamp(yPos, minYPos, maxYPos);

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
