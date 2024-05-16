using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : StateManager
{
    private Rigidbody2D rb;

    public bool facingRight { get; private set; } = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (currentState.GetType() == typeof(WaitState)) rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }

    public bool ObstacleCheck()
    {
        bool wallCheck = Physics2D.Raycast(transform.position, facingRight ? Vector3.right : Vector3.left, 1.2f, LayerMask.GetMask("Ground"));
        bool fallCheck = !Physics2D.Raycast(transform.position + (facingRight ? Vector3.right : Vector3.left) * 1.2f, Vector2.down, .75f, LayerMask.GetMask("Ground"));

        return wallCheck || fallCheck;
    }

    public bool PlayerCheck()
    {
        bool playerCheck = Physics2D.Raycast(transform.position + Vector3.up, facingRight ? Vector3.right : Vector3.left, 1f, LayerMask.GetMask("Player"));

        return playerCheck;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    protected override void SwitchToState(State state) { base.SwitchToState(state); }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (facingRight ? Vector3.right : Vector3.left) * 1.2f);
        Gizmos.DrawRay(transform.position + (facingRight ? Vector3.right : Vector3.left) * 1.2f, Vector2.down * .75f);
    }
}
