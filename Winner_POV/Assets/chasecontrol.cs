using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasecontrol : MonoBehaviour
{
    public fenemymove[] enemyarray;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (fenemymove enemy in enemyarray)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (fenemymove enemy in enemyarray)
            {
                enemy.chase = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
