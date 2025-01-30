using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    private Dictionary<string, ParticleSystem> particle = new();
    [SerializeField] private ParticleSystem[] particles;

    private void Start()
    {
        foreach (ParticleSystem p in particles)
        {
            particle.Add(p.name, p);
        }
    }

    public void Play(string particleName)
    {
        particle[particleName].Play();
    }
    public void Play(ParticleSystem particle)
    {
        particle.Play();
    }
}
