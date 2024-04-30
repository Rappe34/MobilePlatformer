using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    [SerializeField] private EnemyStateManager sm;
    [SerializeField] private WaitState waitState;

    public override State RunCurrentState()
    {
        if (sm.ObstacleCheck())
        {
            waitState.waitFlag = true;
            return waitState;
        }

        sm.Move();

        return this;
    }
}
