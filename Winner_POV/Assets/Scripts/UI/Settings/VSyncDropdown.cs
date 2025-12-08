using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSyncDropdown : MonoBehaviour
{
    [SerializeField] private List<bool> options;
    private Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        dropdown.value = options.IndexOf(settings.GetVsync());
    }

    public void OnValueChanged()
    {
        settings.ToggleVSync(options[dropdown.value]);
    }
}
