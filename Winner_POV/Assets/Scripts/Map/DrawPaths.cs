using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPaths : MonoBehaviour
{
    Button button;
    List<Button> buttons;
    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void ContinuePath(int remainingSteps, int depth, float stepLength, Transform parent, GameObject mapIcon)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + (stepLength * Screen.width), 0);
        DrawPaths nextStep = Instantiate(mapIcon, pos, Quaternion.identity).GetComponent<DrawPaths>();
        nextStep.gameObject.transform.SetParent(parent);

        buttons.Add(nextStep.gameObject.GetComponent<Button>());
        gameObject.GetComponent<EnableNextPoints>().SetNextButtons(buttons);

        if (remainingSteps > 0)
        {
            nextStep.ContinuePath(remainingSteps - 1, depth + 1, stepLength, parent, mapIcon);
        }
    }
}
