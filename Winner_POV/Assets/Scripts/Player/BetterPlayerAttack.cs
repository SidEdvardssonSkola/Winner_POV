using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attack;
    private bora movement;

    private void Start()
    {
        movement = GetComponent<bora>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attack.transform.position = transform.position;
            attack.SetActive(true);
            Invoke(nameof(DisableAttack), 0.05f);
        }
    }

    private void DisableAttack() 
    {
        attack.SetActive(false);
    }
}
