using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireReference : MonoBehaviour
{
    public static GameObject campfireUI;
    private void Start()
    {
        campfireUI = gameObject;
    }
}
