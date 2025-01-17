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
            int direction = movement.FacingDirection;

            attack.transform.eulerAngles = new Vector3(0, 90 + (-direction * 90), 0);

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
