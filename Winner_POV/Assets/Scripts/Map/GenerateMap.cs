using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private int mapLength = 10;

    // StepLenght = avståndet mellan två vertikala punkter
    // värdet är procenten av skärmens höjd
    [SerializeField] private float stepLength = 0.25f;

    //mapWidth = hur långt åt sidan ikonerna kan generaras 
    // värdet är skåärmens bredd i procent
    [SerializeField] private float mapWidth = 0.75f;

    [SerializeField] private int startingPositions = 3;

    //startingHeight = höjden som första ikonerna kommer spawnas i procent av skärmens höjd
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
            xPosition = Screen.width * mapWidth / startingPositions * (i + 1) ;
            GameObject child = Instantiate(mapIcon, new Vector3(xPosition, Screen.height * startingHeight, 0), Quaternion.identity);
            child.transform.SetParent(transform);
        }
    }
}
