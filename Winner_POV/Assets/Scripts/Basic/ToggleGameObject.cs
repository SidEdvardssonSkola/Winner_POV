using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToToggle;
    public void ToggleObjects()
    {
        foreach (GameObject o in objectsToToggle)
        {
            if (o.active)
            {
                o.SetActive(false);
            }
            else
            {
                o.SetActive(true);
            }
        }
    }
    public void HideObjects()
    {
        foreach (GameObject o in objectsToToggle)
        {
            o.SetActive(false);
        }
    }
    public void ShowObjects()
    {
        foreach (GameObject o in objectsToToggle)
        {
            o.SetActive(true);
        }
    }
}
