using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private int mapLength = 10;

    // StepLenght = avståndet mellan två vertikala punkter
    // värdet är procenten av skärmens höjd
    [SerializeField] private float stepLength = 1.5f;

    //mapWidth = hur långt åt sidan ikonerna kan generaras
    [SerializeField] private float mapWidth = 8.5f;

    [SerializeField] private int startingPositions = 3;

    //startingHeight = höjden som första ikonerna kommer spawnas;
    [SerializeField] private float startingHeight = -3.5f;



    [SerializeField] private GameObject mapIcon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTheMap();
        }
    }

    private void GenerateTheMap()
    {
        float xPosition;

        List<Button> startingButtons = new();
        for (int i = 0; i < startingPositions; i++)
        {
            xPosition = mapWidth / startingPositions * (i + 1) - (mapWidth - mapWidth / startingPositions);

            GameObject nextStep = Instantiate(mapIcon, new Vector3(xPosition, startingHeight, 0), Quaternion.identity, transform);
            nextStep.GetComponent<DrawPaths>().ContinuePath(mapLength - 1, 1, stepLength, transform, mapIcon);

            startingButtons.Add(nextStep.GetComponent<Button>());
            nextStep.GetComponent<Button>().interactable = true;
        }
        foreach(Button b in startingButtons)
        {
            b.AddComponent<DisableOtherActiveButtons>();
            b.GetComponent<DisableOtherActiveButtons>().SetButtonsToDisable(startingButtons.ToArray());
            UnityAction addToOnClick;
            addToOnClick = b.GetComponent<DisableOtherActiveButtons>().DisableButtons;
            b.onClick.AddListener(addToOnClick);
        }
    }
}
