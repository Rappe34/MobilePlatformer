using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class WaitState : State
{
    [SerializeField] private float averageWaitTime = 2.2f;
    [SerializeField] private float waitTimeFluctuation = 1.2f;

    private EnemyStateManager sm;
    private RoamState roamState;
    private AttackState attackState;
    private Animator anim;

    private bool waiting = false;
    private float waitTimer = 0f;

    private void Awake()
    {
        sm = GetComponent<EnemyStateManager>();
        roamState = GetComponent<RoamState>();
        attackState = GetComponent<AttackState>();
        anim = GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {
        if (sm.PlayerCheck()) return StateEnd(attackState);

        if (waiting)
        {
            if (waitTimer <= 0)
            {
                waiting = false;
                sm.Flip();
            }
            else
            {
                waitTimer -= Time.deltaTime;
            }
        }
        else return StateEnd(roamState);

        return this;
    }

    public override void StateStartFlag()
    {
        waiting = true;
        waitTimer = Random.Range(averageWaitTime - waitTimeFluctuation, averageWaitTime + waitTimeFluctuation);
        anim.SetBool("Waiting", true);
    }

    protected override State StateEnd(State state)
    {
        waiting = false;
        anim.SetBool("Waiting", false);

        state.StateStartFlag();
        return state;
    }
}
