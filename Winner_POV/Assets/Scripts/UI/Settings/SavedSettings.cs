using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedSettings
{
    //Video
    public Vector2 resolution;
    public int fPS;
    public bool vSync;
    public bool fullScreen;

    //Audio
    public float volume = 100;
}
