using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    [SerializeField] private float averageWaitTime = 2.2f;
    [SerializeField] private float waitTimeFluctuation = 1.2f;

    private EnemyStateManager sm;
    private RoamState roamState;

    private bool waitFlag = false;
    private bool isWaiting = false;

    private void Awake()
    {
        sm = GetComponent<EnemyStateManager>();
        roamState = GetComponent<RoamState>();
    }

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

    public void SetWaitFlag(bool value)
    {
        waitFlag = value;
    }
}
