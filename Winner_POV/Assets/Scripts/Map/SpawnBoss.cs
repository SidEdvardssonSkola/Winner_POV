using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    Button button;
    private bool playSoundWhenEnabled = false;

    private void OnEnable()
    {
        if (playSoundWhenEnabled)
        {
            GetComponent<AudioSource>().Play();
        }    
    }

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

    [SerializeField] private Vector3 spawnPos;
    public void WhenClicked()
    {
        button.enabled = false;

        Transform player = GameObject.Find("Player (1)").GetComponent<Transform>();
        player.transform.position = spawnPos;

        playSoundWhenEnabled = true;
    }
}
