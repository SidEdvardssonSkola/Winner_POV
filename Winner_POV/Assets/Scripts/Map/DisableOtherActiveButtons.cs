using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOtherActiveButtons : MonoBehaviour
{
    Button[] buttons;
    public void SetButtonsToDisable(Button[] buttonsToSet)
    {
        buttons = buttonsToSet;   
    }
    public void DisableButtons()
    {
        foreach(Button b in buttons)
        {
            b.interactable = false;
        }
    }
}
