using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RegisterButtonToRegistry : MonoBehaviour
{
    private MapIconManager mapManager;
    public void Register(int file, int depth)
    {
        mapManager = GetComponentInParent<MapIconManager>();
        mapManager.AddButtonToManager(new Vector2(file, depth), GetComponent<Button>());
    }
}
