using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToReturn;
    public GameObject GetObject()
    {
        return objectToReturn;
    }
}
