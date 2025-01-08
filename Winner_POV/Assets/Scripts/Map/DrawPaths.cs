using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPaths : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }
    public void ContinuePath(int remainingSteps, int depth, float stepLength, Transform parent, GameObject mapIcon)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + (stepLength * Screen.width), 0);
        DrawPaths nextStep = Instantiate(mapIcon, pos, Quaternion.identity).GetComponent<DrawPaths>();
        nextStep.gameObject.transform.SetParent(parent);

        if (remainingSteps > 0)
        {
            nextStep.ContinuePath(remainingSteps - 1, depth + 1, stepLength, parent, mapIcon);
        }
    }
}
