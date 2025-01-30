using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public static void Play(AudioSource audio)
    {
        audio.Play();
    }
}
