using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    [SerializeField] private List<Vector2> options = new();
    private TMP_Dropdown dropdown;

    private Settings settings;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        if (!options.Contains(new(Screen.width, Screen.height)) )
        {
            dropdown.value = 8;
            return;
        }

        dropdown.value = options.IndexOf(new(Screen.width, Screen.height));
    }

    public void OnValueChanged()
    {
        if (options[dropdown.value].x <= 0 || options[dropdown.value].y <= 0)
        {
            Vector2 newResolution = new(Screen.mainWindowDisplayInfo.width, Screen.mainWindowDisplayInfo.height);

            settings.SetNewResolution(newResolution);
            return;
        }

        settings.SetNewResolution(options[dropdown.value]);
    }
}
