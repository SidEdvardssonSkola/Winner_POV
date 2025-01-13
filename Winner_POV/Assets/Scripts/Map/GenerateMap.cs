using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

<<<<<<< HEAD
<<<<<<< HEAD
=======
    void Start()
    {
        
    }

>>>>>>> parent of 5248dee (test)
=======
    [SerializeField] private float speed = 5;

>>>>>>> parent of fbf66cd (Ã„ndrade nÃ¥gra fÃ¥ instÃ¤llningar pÃ¥ generationen, tror fortfarande att det kan behÃ¶vas ytterligare Ã¤ndringar)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GenerateTheMap());
        }
    }

    private IEnumerator GenerateTheMap()
    {
        Debug.Log("Generating Map");
        float xPosition;
        for (int i = 0; i < startingPositions; i++)
        {
            xPosition = Screen.width * mapWidth / startingPositions * (i + 1);
            GameObject nextStep = Instantiate(mapIcon, new Vector3(xPosition, Screen.height * startingHeight, 0), Quaternion.identity);
            nextStep.transform.SetParent(transform);
            nextStep.GetComponent<DrawPaths>().ContinuePath(mapLength - 1, 1, stepLength, transform, mapIcon);

<<<<<<< HEAD
            startingButtons.Add(nextStep.GetComponent<Button>());

            yield return new WaitForSeconds(0.1f);
        }
        foreach(Button b in startingButtons)
        {
            b.AddComponent<DisableOtherActiveButtons>();
            b.GetComponent<DisableOtherActiveButtons>().SetButtonsToDisable(startingButtons.ToArray());
            UnityAction addToOnClick;
            addToOnClick = b.GetComponent<DisableOtherActiveButtons>().DisableButtons;
            b.onClick.AddListener(addToOnClick);

            b.interactable = true;
=======
            nextStep.gameObject.GetComponent<Button>().enabled = true;
>>>>>>> parent of 5248dee (test)
        }
    }
}
