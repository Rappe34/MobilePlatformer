using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    [SerializeField] private EnemyStateManager sm;
    [SerializeField] private RoamState roamState;
    [SerializeField] private float averageWaitTime = 2.2f;
    [SerializeField] private float waitTimeFluctuation = 1.2f;

    public bool waitFlag = false;

    private bool isWaiting = false;

    public override State RunCurrentState()
    {
        if (waitFlag) StartCoroutine(Wait());

        if (!isWaiting)
            return roamState;

        return this;
    }

    private IEnumerator Wait()
    {
        waitFlag = false;
        isWaiting = true;

        float randomWaitTime = Random.Range(averageWaitTime - waitTimeFluctuation, averageWaitTime + waitTimeFluctuation);
        yield return new WaitForSeconds(randomWaitTime);

        isWaiting = false;
        sm.Flip();
    }
}
