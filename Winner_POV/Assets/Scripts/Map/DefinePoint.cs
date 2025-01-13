using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefinePoint : MonoBehaviour
{
    private Image image;
    enum PointTypes
    {
        Enemy = 0,
        Campfire = 1,
        Miniboss = 2,
        Boss = 3,
        LootRoom = 4,
        Shop = 5
    }

    public void Define(int depth, int remainingSteps)
    {
        int totalSteps = remainingSteps + depth;
        if (remainingSteps == 0)
        {
            SetPointTo(PointTypes.Campfire);
            return;
        }

        if (remainingSteps == Mathf.Round(totalSteps / 2))
        {
            if (Random.Range(1, 4) < 3)
            {
                //miniboss
                SetPointTo(PointTypes.Miniboss);
                return;
            }
        }

        if (remainingSteps == Mathf.Round(totalSteps / 2) - 1)
        {
            if (Random.Range(1, 4) < 3)
            {
                //campfire
                SetPointTo(PointTypes.Campfire);
                return;
            }
        }

        if (depth == 1)
        {
            //normal enemy
            SetPointTo(PointTypes.Enemy);
            return;
        }

        //annars slump
    }

    private void SetPointTo(PointTypes pointType)
    {
        image = GetComponent<Image>();

        switch ((int)pointType)
        {
            //enemy
            case 0:
                image.color = Color.red;
                break;

            //campfire
            case 1:
                image.color = Color.yellow;
                break;

            //miniboss
            case 2:
                image.color = Color.magenta;
                break;

            //boss
            case 3:
                image.color = Color.black;
                break;

            //lootroom
            case 4:
                image.color = Color.green;
                break;

            //shop
            case 5:
                image.color = Color.cyan;
                break;
        }
    }
}
