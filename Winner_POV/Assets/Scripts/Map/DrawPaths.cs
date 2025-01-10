using System.Collections;

using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawPaths : MonoBehaviour
{
    Button button;
    [SerializeField] GameObject lineDrawerObject;
    [SerializeField] private float branchChance = 0.65f;
    [SerializeField] private float mergeChance = 0.4f;
    [SerializeField] private int maxBranchLength = 3;
    [SerializeField] private float branchOffset = 1.25f;
    private void Start()
    {
        button = GetComponent<Button>();
    }

    //Metoden för oförgrenade stigar
    public void ContinuePath(int file, int remainingSteps, int depth, float stepLength, Transform parent, GameObject mapIcon, int branchCooldown)
    {

        parent.GetComponent<MapIconManager>().AddButtonToManager(new Vector2(file, depth), GetComponent<Button>());


        if (remainingSteps > 0)
        {
            //Genererar nästa steg i huvudgrenen

            Vector3 pos = new(transform.position.x, transform.position.y + stepLength, 0);
            DrawPaths nextStep = Instantiate(mapIcon, pos, Quaternion.identity, parent).GetComponent<DrawPaths>();

            //ritar en linje till nästa steg

            LineDrawer lineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
            lineDrawer.DrawLine(transform.position, nextStep.transform.position);

            //rullar en tärning för att se om stigen kommer att förgrenas

            if (Random.Range(0f, 1f) < branchChance && branchCooldown < 1)
            {
                //ifall tärningen lyckas

                nextStep.ContinuePath(file, remainingSteps - 1, depth + 1, stepLength, parent, mapIcon, 3);

                int branchDirection;
                if (Random.Range(1, 3) == 1)
                {
                    branchDirection = 1;
                }
                else
                {
                    branchDirection = -1;
                }

                DrawPaths branchNextStep;

                if (parent.GetComponent<MapIconManager>().GetButtonFromIndex(new Vector2(depth + 1, file + branchDirection)) == null)
                {
                    Vector3 branchPos = new(transform.position.x + (branchDirection * branchOffset), transform.position.y + stepLength, 0);
                    branchNextStep = Instantiate(mapIcon, branchPos, Quaternion.identity, parent).GetComponent<DrawPaths>();

                    //ritar en linje till nästa steg

                    LineDrawer branchLineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
                    branchLineDrawer.DrawLine(transform.position, branchNextStep.transform.position);

                    branchNextStep.ContinuePath(file + branchDirection, remainingSteps - 1, depth + 1, stepLength, parent, mapIcon, true, 1);
                }
                else
                {
                    branchNextStep = parent.GetComponent<MapIconManager>().GetButtonFromIndex(new Vector2(depth + 1, file + branchDirection)).GetComponent<DrawPaths>();
                }

                Button[] nextSteps = { nextStep.GetComponent<Button>(), branchNextStep.GetComponent<Button>() };

                foreach (Button b in nextSteps)
                {
                    b.AddComponent<DisableOtherActiveButtons>();
                    b.GetComponent<DisableOtherActiveButtons>().SetButtonsToDisable(nextSteps);
                    UnityAction addToOnClick;
                    addToOnClick = b.GetComponent<DisableOtherActiveButtons>().DisableButtons;
                    b.onClick.AddListener(addToOnClick);
                }

                GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);
            }
            else
            {
                //ifall den inte lyckas

                nextStep.ContinuePath(file, remainingSteps - 1, depth + 1, stepLength, parent, mapIcon, branchCooldown - 1);

                Button[] nextSteps = { nextStep.GetComponent<Button>() };
                GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);
            }

        }
        else
        {
            EndPath(stepLength);
        }
    }

    // Overload metod för förgrenade stigar
    public void ContinuePath(int file, int remainingSteps, int depth, float stepLength, Transform parent, GameObject mapIcon, bool isBranch, int branchLength)
    {
        if (remainingSteps > 0)
        {
            if (Random.Range(0f, 1f) < mergeChance || maxBranchLength <= branchLength)
            {
                StartCoroutine(MergePath(parent.gameObject, file, depth));
            }
            else
            {
                Vector3 pos = new(transform.position.x, transform.position.y + stepLength, 0);

                DrawPaths nextStep = Instantiate(mapIcon, pos, Quaternion.identity, parent).GetComponent<DrawPaths>();

                LineDrawer lineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
                lineDrawer.DrawLine(transform.position, nextStep.transform.position);

                nextStep.ContinuePath(file, remainingSteps - 1, depth + 1, stepLength, parent, mapIcon, true, branchLength + 1);

                Button[] nextSteps = { nextStep.GetComponent<Button>() };
                GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);
            }
        }
        else
        {
            EndPath(stepLength);
        }
    }

    private void EndPath(float stepLength)
    {
        LineDrawer lineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
        lineDrawer.DrawLine(transform.position, new Vector2(0, transform.position.y + stepLength));
    }

    private IEnumerator MergePath(GameObject parent, int file, int depth)
    {
        yield return new WaitForSeconds(0.1f);
        Button[] nextSteps;
        if (Random.Range(1, 3) == 1 && file > 0 && file < (parent.GetComponent<GenerateMap>().startingPositions - 1) * 2 + 2)
        {
            //Mergea till båda sidorna
            nextSteps = new Button[]{ parent.GetComponent<MapIconManager>().GetButtonFromIndex(new Vector2(file + 1, depth + 1)), parent.GetComponent<MapIconManager>().GetButtonFromIndex(new Vector2(file - 1, depth + 1)) };

            foreach (Button b in nextSteps)
            {
                b.AddComponent<DisableOtherActiveButtons>();
                b.GetComponent<DisableOtherActiveButtons>().SetButtonsToDisable(nextSteps);
                UnityAction addToOnClick;
                addToOnClick = b.GetComponent<DisableOtherActiveButtons>().DisableButtons;
                b.onClick.AddListener(addToOnClick);
            }

            GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);
        }
        else
        {
            int mergeDirection;
            if (Random.Range(1, 3) == 1)
            {
                mergeDirection = 1;
            }
            else
            {
                mergeDirection = -1;
            }
            nextSteps = new Button[]{ parent.GetComponent<MapIconManager>().GetButtonFromIndex(new Vector2(Mathf.Clamp(file + mergeDirection, 1, (parent.GetComponent<GenerateMap>().startingPositions - 1) * 2 + 1), depth + 1)) };
            GetComponent<EnableNextPoints>().SetNextButtons(nextSteps);
        }
        foreach(Button b in nextSteps)
        {
            LineDrawer lineDrawer = Instantiate(lineDrawerObject, transform.position, Quaternion.identity).GetComponent<LineDrawer>();
            lineDrawer.DrawLine(transform.position, b.transform.position);
        }
    }
}
