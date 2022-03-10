using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private State DefaultState;
    [HideInInspector] public State CurrentState;

    public void Init()
    {
        if (DefaultState != null) { StateChange(DefaultState); }
    }

    public void ProcessStates()
    {
        if(CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    public void StateChange(State toState)
    {
        if(CurrentState != null) { CurrentState.OnStateExit(); } // Exit previous state //
        CurrentState = toState; // Update current state to given state //
        CurrentState.OnStateEnter(); // Process current state enter //
    }
}
