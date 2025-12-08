using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    public void ToggleObjects()
    {
        foreach (GameObject o in objectsToToggle)
        {
            o.SetActive(!o.activeSelf);
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
