using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attack;
    [SerializeField] private float attackCooldown = 0.15f;
    bool canAttack = true;
    private bora movement;

    [SerializeField] private AudioSource swoshSound;

    private void Start()
    {
        movement = GetComponent<bora>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            canAttack = false;

            swoshSound.pitch = Random.Range(0.75f, 1.25f);
            swoshSound.Play();

            int direction = movement.FacingDirection;

            attack.GetComponentInParent<Transform>().eulerAngles = new Vector3(0, 90 + (-direction * 90), 0);

            attack.transform.position = transform.position;
            attack.SetActive(true);
            Invoke(nameof(DisableAttack), attackCooldown);
        }
    }

    private void DisableAttack() 
    {
        canAttack = true;
        attack.SetActive(false);
    }
}
