using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    [SerializeField] private float movementSpeed = 3f;
    private EnemyStateManager sm;
    private WaitState waitState;
    private AttackState attackState;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        sm = GetComponent<EnemyStateManager>();
        waitState = GetComponent<WaitState>();
        attackState = GetComponent<AttackState>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {
        if (sm.PlayerCheck())
        {
            attackState.AttackFlag();
            return attackState;
        }

        if (sm.ObstacleCheck())
        {
            waitState.WaitFlag();
            return waitState;
        }

        rb.velocity = new Vector2(sm.facingRight ? movementSpeed : -movementSpeed, rb.velocity.y);

        anim.SetBool("Waiting", false);

        return this;
    }
}
