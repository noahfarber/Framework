using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace Framework
{
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
            if (CurrentState != null)
            {
                State UpdatedState = CurrentState.OnUpdate();

                if(UpdatedState != CurrentState)
                {
                    StateChange(UpdatedState);
                }
            }
        }

        public void StateChange(State toState)
        {
            if (CurrentState != null) { CurrentState.OnStateExit(); } // Exit previous state // 
            Debugger.Instance.Log("State Exit: " + CurrentState.ToString());
            CurrentState = toState; // Update current state to given state //
            CurrentState.OnStateEnter(); // Process current state enter //
            Debugger.Instance.Log("State Enter: " + CurrentState.ToString());
        }
    }
}