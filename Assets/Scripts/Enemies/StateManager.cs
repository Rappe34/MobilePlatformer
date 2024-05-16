using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    [SerializeField] private State startState;
    public State currentState { get; private set; }
    public State previousState { get; private set; }

    private void Start()
    {
        SwitchToState(startState);
    }

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null && nextState != currentState)
        {
            SwitchToState(nextState);
        }
    }

    protected virtual void SwitchToState(State state)
    {
        previousState = currentState;
        currentState = state;
    }
}
