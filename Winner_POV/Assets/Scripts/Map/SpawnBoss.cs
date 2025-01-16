using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();

        ToggleGameObject show = gameObject.AddComponent<ToggleGameObject>();
        show.objectsToToggle = new GameObject[] { GameObject.Find("BossRoom").GetComponent<GetGameObject>().GetObject() };

        button.onClick.AddListener(show.ShowObjects);

        ToggleGameObject hide = gameObject.AddComponent<ToggleGameObject>();
        hide.objectsToToggle = new GameObject[] { GameObject.Find("map") };

        button.onClick.AddListener(hide.HideObjects);
    }
}
