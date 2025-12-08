using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeText;
    private Slider volumeSlider;
    private Settings settings;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        settings = GameObject.FindWithTag("Settings Manager").GetComponent<Settings>();

        volumeSlider.value = settings.GetVolumePercentage();
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        volumeText.text = $"Volume: {Mathf.Round(volumeSlider.value * 100)}%";
        settings.SetNewVolume(volumeSlider.value);
    }
}
