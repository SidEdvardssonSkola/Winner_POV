using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    UnityEvent OnDeath { get; set; }
    UnityEvent OnDamageTaken { get; set; }

    float MaxHealth { get; set; }
    float Health { get; set; }

    float IFramesInSeconds { get; set; }
    bool IsIFrameActive { get; set; }

    void ChangeHealth(float ammount);

    void RemoveIFrames();

    void ChangeHealth(float ammount, bool ignoreIframes);

    void Die();
}
