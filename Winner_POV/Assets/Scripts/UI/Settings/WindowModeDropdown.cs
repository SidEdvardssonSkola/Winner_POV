using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowModeDropdown : MonoBehaviour
{
    [SerializeField] private List<int> options;
    private Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        dropdown.value = options.IndexOf((int)Screen.fullScreenMode);
    }

    public void OnValueChanged()
    {
        df
    }
}
