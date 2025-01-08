using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableNextPoints : MonoBehaviour
{
    Button[] buttons;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();   
    }

    public void SetNextButtons(List<Button> buttonsToSet)
    {
        buttons = buttonsToSet.ToArray();
    }

    public void EnableTheNextPoints()
    {
        button.interactable = false;

        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }
}
