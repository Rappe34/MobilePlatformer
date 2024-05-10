using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : StateManager
{
    [SerializeField] private DeathState deathState;

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
        bool wallCheck = Physics2D.Raycast(transform.position - new Vector3(0, -.5f), facingRight ? Vector3.right : Vector3.left, 1.2f, LayerMask.GetMask("Ground"));
        bool fallCheck = !Physics2D.Raycast(transform.position + (facingRight ? Vector3.right : Vector3.left) * 1.2f, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));

        return wallCheck || fallCheck;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    protected override void SwitchToState(State state) { base.SwitchToState(state); }

    private void Die()
    {
        deathState.DeathFlag();
        SwitchToState(deathState);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, -.5f), (facingRight ? Vector3.right : Vector3.left) * 1.2f);
        Gizmos.DrawRay(transform.position + (facingRight ? Vector3.right : Vector3.left) * 1.2f, Vector2.down * 1.5f);
    }
}
