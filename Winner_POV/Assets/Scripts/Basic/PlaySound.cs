using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void Play(AudioSource audio)
    {
        audio.pitch = 1f;
        audio.Play();
    }

    public void PlayWithRandomPitch(AudioSource audio)
    {
        audio.pitch = Random.Range(0.75f, 1.25f);
        audio.Play();
    }

    [SerializeField] private AudioSource[] audioSource;
    public void PlayAudioFromIndex(int index)
    {
        audioSource[index].pitch = Random.Range(0.75f, 1.25f);
        audioSource[index].Play();
    }
}
