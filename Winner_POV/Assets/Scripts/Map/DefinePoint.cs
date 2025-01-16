using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PointSpawnWeight
{
    public string name;
    public int value;
}

public class DefinePoint : MonoBehaviour
{
    [Header("Spawnweight Only Applies to Randomized Points on the map")]
    [SerializeField] private PointSpawnWeight[] spawnWeight;

    [Header("Name Shall Refer to the Parent of The Canvas")]
    [SerializeField] private string map = "map";

    private Image image;
    private Button button;

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
            if (UnityEngine.Random.Range(1, 4) < 3)
            {
                //miniboss
                SetPointTo(PointTypes.Miniboss);
                return;
            }
        }

        if (remainingSteps == Mathf.Round(totalSteps / 2) - 1)
        {
            if (UnityEngine.Random.Range(1, 4) < 3)
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

        List<int> realSpawnWeight = new();
        int loops = 0;
        for (int i = 0; i < spawnWeight.Length; i++)
        {
            realSpawnWeight.Add(spawnWeight[loops].value);

            loops++;
        }

        int randomNumber = CustomRandom.WeightedRandom(realSpawnWeight.ToArray());
        SetPointTo((PointTypes)randomNumber);
    }

    private void SetPointTo(PointTypes pointType)
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        switch ((int)pointType)
        {
            //enemy
            case 0:
                image.color = Color.red;
                break;

            //campfire
            case 1:
                Campfire();
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

    [SerializeField] private string CampfireUiName = "Campfire";
    private void Campfire()
    {
        image.color = Color.yellow;

        ToggleGameObject hide = gameObject.AddComponent<ToggleGameObject>();
        hide.objectsToToggle = new GameObject[] { GameObject.Find(map) };
        button.onClick.AddListener(hide.HideObjects);

        ToggleGameObject show = gameObject.AddComponent<ToggleGameObject>();
        show.objectsToToggle = new GameObject[] { GameObject.Find(CampfireUiName).GetComponent<GetGameObject>().GetObject() };
        button.onClick.AddListener(show.ShowObjects);
    }
}
