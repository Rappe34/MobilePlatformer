using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : StateManager
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public bool facingRight { get; private set; } = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (currentState.GetType() == typeof(WaitState)) rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }

    public bool ObstacleCheck()
    {
        bool wallCheck = Physics2D.Raycast(transform.position, facingRight ? Vector3.right : Vector3.left, 1.2f, LayerMask.GetMask("Ground"));
        bool fallCheck = !Physics2D.Raycast(transform.position + (facingRight ? Vector3.right * 1.2f : Vector3.left * 1.2f), Vector2.down, 1.2f, LayerMask.GetMask("Ground"));

        return wallCheck || fallCheck;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    public void Move()
    {
        rb.velocity = new Vector2(facingRight ? speed : -speed, rb.velocity.y);
    }
}
