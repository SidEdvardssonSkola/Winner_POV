using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.color;

        Rb = GetComponent<Rigidbody2D>();

        enemyStateMachine.Init(idleState);
    }
    void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();
    }

    public void Flash()
    {
        spriteRenderer.color = Color.red;

        Invoke(nameof(UnFlash), IFramesInSeconds);
    }
    private void UnFlash()
    {
        CancelInvoke(nameof(UnFlash));
        spriteRenderer.color = originalcolor;
    }
}
