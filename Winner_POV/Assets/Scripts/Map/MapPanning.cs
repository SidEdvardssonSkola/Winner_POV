using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanning : MonoBehaviour
{
    [SerializeField] private float panSpeed = 5.5f;
    private void Update()
    {
        transform.Translate(panSpeed * -Input.GetAxis("Horizontal") * Time.deltaTime, panSpeed * -Input.GetAxis("Vertical") * Time.deltaTime, 0);
    }
}
