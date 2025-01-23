using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCaller : MonoBehaviour
{
    [SerializeField] private UnityEvent whenCalled;

    public void Call()
    {
        whenCalled.Invoke();
    }
}
