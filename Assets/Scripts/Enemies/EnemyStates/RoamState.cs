using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

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
            return StateEnd(attackState);
        }

        if (sm.ObstacleCheck())
        {
            return StateEnd(waitState);
        }

        rb.velocity = new Vector2(sm.facingRight ? movementSpeed : -movementSpeed, rb.velocity.y);

        return this;
    }

    public override void StateStartFlag()
    {
        anim.SetBool("Walking", true);
    }

    protected override State StateEnd(State state)
    {
        anim.SetBool("Walking", false);

        state.StateStartFlag();
        return state;
    }
}
