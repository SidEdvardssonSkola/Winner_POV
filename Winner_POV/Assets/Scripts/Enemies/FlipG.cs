using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipG : MonoBehaviour
{
    private genemymovement movement;
    private SpriteRenderer sprite;
    void Start()
    {
        movement = GetComponent<genemymovement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movement.direction < 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}
