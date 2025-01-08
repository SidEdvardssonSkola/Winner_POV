using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private int mapLength = 10;

    // StepLenght = avst�ndet mellan tv� vertikala punkter
    // v�rdet �r procenten av sk�rmens h�jd
    [SerializeField] private float stepLength = 0.25f;

    //mapWidth = hur l�ngt �t sidan ikonerna kan generaras 
    // v�rdet �r sk��rmens bredd i procent
    [SerializeField] private float mapWidth = 0.75f;

    [SerializeField] private int startingPositions = 3;

    //startingHeight = h�jden som f�rsta ikonerna kommer spawnas i procent av sk�rmens h�jd
    [SerializeField] private float startingHeight = 0.2f;



    [SerializeField] private GameObject mapIcon;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTheMap();
        }
    }

    private void GenerateTheMap()
    {
        Debug.Log("Generating Map");
        float xPosition;
        for (int i = 0; i < startingPositions; i++)
        {
            xPosition = Screen.width * mapWidth / startingPositions * (i + 1);
            GameObject nextStep = Instantiate(mapIcon, new Vector3(xPosition, Screen.height * startingHeight, 0), Quaternion.identity);
            nextStep.transform.SetParent(transform);
            nextStep.GetComponent<DrawPaths>().ContinuePath(mapLength - 1, 1, stepLength, transform, mapIcon);

            nextStep.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}
