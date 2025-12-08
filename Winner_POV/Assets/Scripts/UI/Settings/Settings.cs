using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private string settingsFilePath;

    [SerializeField] private SavedSettings defaultSettings;
    private SavedSettings settings;
    private string settingsJson;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float minVolume = -80f;
    [SerializeField] private float maxVolume = 20f;

    [SerializeField] private bool loadAllSettingsOnAwake = true;

    private void Awake()
    {
        if (!File.Exists(settingsFilePath))
        {
            settings = defaultSettings;
        }
        else
        {
            settingsJson = File.ReadAllText(settingsFilePath);
            settings = JsonUtility.FromJson<SavedSettings>(settingsJson);
        }

        if (loadAllSettingsOnAwake)
        {
            LoadAllSettings();
        }
        else
        {
            LoadAudioSettings();
        }
    }

    public void SaveSettings()
    {
        settingsJson = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsFilePath, settingsJson);
    }

    public void LoadAllSettings()
    {
        SetNewFPS(settings.fPS);
        SetNewResolution(settings.resolution);
        ToggleVSync(settings.vSync);
        SetNewWindowMode(settings.fullScreenMode);
    }

    public void LoadAudioSettings()
    {
        
    }

    public void SetNewFPS(int fPS)
    {
        if (fPS <= 0)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = fPS;
        }
    }

    public void SetNewResolution(Vector2 resolution)
    {
        if (resolution == null || resolution.x <= 0 || resolution.y <= 0)
        {
            resolution.x = Display.main.systemWidth;
            resolution.y = Display.main.systemHeight;
        }

        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreenMode);
    }

    public void SetNewResolution(int xResolution, int yResolution)
    {
        if (xResolution <= 0 || yResolution <= 0)
        {
            xResolution = Display.main.systemWidth;
            yResolution = Display.main.systemHeight;
        }

        Screen.SetResolution(xResolution, yResolution, Screen.fullScreenMode);
    }

    public void ToggleVSync(bool vSync)
    {
        QualitySettings.vSyncCount = vSync ? 1 : 0;
    }

    public bool GetVsync()
    {
        return QualitySettings.vSyncCount > 0;
    }

    public void SetNewWindowMode(FullScreenMode windowMode)
    {
        Screen.fullScreenMode = windowMode;
    }

    public void SetNewVolume(float volume)
    {
        if (mixer == null) return;

        volume = Mathf.Clamp01(volume);
        float newVolume = ((maxVolume - minVolume) * volume) + minVolume;
        mixer.SetFloat("masterVolume", newVolume);
    }

    public float GetVolumePercentage()
    {
        float currentVolume;
        bool g = mixer.GetFloat("masterVolume", out currentVolume);

        float percentage;
        if (currentVolume - minVolume != 0) percentage = (currentVolume - minVolume) / (maxVolume - minVolume);
        else percentage = 0;

            return percentage;
    }
}
