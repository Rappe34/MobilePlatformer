using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class RoamState : State
{
    private EnemyStateManager sm;
    private WaitState waitState;

    private void Awake()
    {
        sm = GetComponent<EnemyStateManager>();
        waitState = GetComponent<WaitState>();
    }

    public override State RunCurrentState()
    {
        if (sm.ObstacleCheck())
        {
            waitState.SetWaitFlag(true);
            return waitState;
        }

        sm.Move();

        return this;
    }
}
