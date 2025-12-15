using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VSyncDropdown : MonoBehaviour
{
    [SerializeField] private List<bool> options = new();
    [SerializeField] private TMP_Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        dropdown.value = options.IndexOf(settings.GetVsync());
    }

    public void OnValueChanged()
    {
        settings.ToggleVSync(options[dropdown.value]);
    }
}
