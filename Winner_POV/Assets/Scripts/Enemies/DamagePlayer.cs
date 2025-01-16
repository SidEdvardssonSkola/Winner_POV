using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] bool isDPS = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isDPS)
            {
                collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage * Time.deltaTime, true);
            }
        }
    }
}
