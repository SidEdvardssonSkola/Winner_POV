using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] private KeyCode[] combination;
    bool shouldLoop = true;

    public UnityEvent onCombinationSucces;

    void Start()
    {
        StartCoroutine(CheckForKonamiCode());
    }

    private IEnumerator CheckForKonamiCode()
    {
        while (shouldLoop)
        {

            foreach (KeyCode k in combination)
            {

                yield return new WaitUntil(() => !Input.anyKey);

                yield return new WaitUntil(() => Input.anyKey);

                if (!Input.GetKey(k))
                {
                    shouldLoop = true;
                    break;
                }

                shouldLoop = false;
            }
        }

        onCombinationSucces.Invoke();
    }
}
