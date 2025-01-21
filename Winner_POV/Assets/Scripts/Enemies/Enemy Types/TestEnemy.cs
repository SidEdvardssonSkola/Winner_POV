using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    public override void Start()
    { 
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.color;

        Rb = GetComponent<Rigidbody2D>();

        enemyStateMachine.Init(idleState);
    }
    void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();
    }
}
