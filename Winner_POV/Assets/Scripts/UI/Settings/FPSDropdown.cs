using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSDropdown : MonoBehaviour
{
    [SerializeField] private List<int> framerates = new();
    private TMP_Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        int currentFPS = -1;

        foreach (int i in framerates)
        {
            if (i == Application.targetFrameRate)
            {
                currentFPS = Application.targetFrameRate;
            }
        }

        dropdown.value = framerates.IndexOf(currentFPS);
    }

    public void OnValueChanged()
    {
        settings.SetNewFPS(framerates[dropdown.value]);
    }
}
