using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    public void Play()
    {
        animator.Play(animationName);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Play();
        }
    }
}
