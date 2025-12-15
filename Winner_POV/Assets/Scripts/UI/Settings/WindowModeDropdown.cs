using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowModeDropdown : MonoBehaviour
{
    [SerializeField] private List<bool> options = new();
    private TMP_Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        dropdown.value = options.IndexOf(Screen.fullScreen);
    }

    public void OnValueChanged()
    {
        settings.SetNewWindowMode(options[dropdown.value]);
    }
}
