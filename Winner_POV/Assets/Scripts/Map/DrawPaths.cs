using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPaths : MonoBehaviour
{
    Button button;
    [SerializeField] GameObject lineDrawerObject;
    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void ContinuePath(int remainingSteps, int depth, float stepLength, Transform parent, GameObject mapIcon)
    {
        Vector3 pos = new(transform.position.x, transform.position.y + stepLength, 0);
        DrawPaths nextStep = Instantiate(mapIcon, pos, Quaternion.identity, parent).GetComponent<DrawPaths>();

        LineDrawer lineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
        lineDrawer.DrawLine(transform.position, nextStep.transform.position);

        Button[] nextSteps = {nextStep.GetComponent<Button>()};
        GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);

        if (remainingSteps > 0)
        {
            nextStep.ContinuePath(remainingSteps - 1, depth + 1, stepLength, parent, mapIcon);
        }
    }
}
