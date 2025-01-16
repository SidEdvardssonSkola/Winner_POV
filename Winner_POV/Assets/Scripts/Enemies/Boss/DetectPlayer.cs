using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    BossController bossController;
    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossController.UpdateIsPlayerInRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossController.UpdateIsPlayerInRange(false);
        }
    }
}
