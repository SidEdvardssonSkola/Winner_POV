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
            print("beginListen");

            foreach (KeyCode k in combination)
            {
                print("Waiting for release of key");
                yield return new WaitUntil(() => !Input.anyKey);

                print("Listening");
                yield return new WaitUntil(() => Input.anyKey);

                if (Input.GetKey(k))
                {
                    print("dading");
                }
                else
                {
                    print("restart");
                    break;
                }

                shouldLoop = false;
            }
        }
        print("success");
        onCombinationSucces.Invoke();
    }
}
