using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    [SerializeField] private  Animator anim;
    [SerializeField] private string animation;

    public void Play()
    {
        anim.SetTrigger(animation);
    }
}
