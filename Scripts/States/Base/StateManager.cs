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

                if(UpdatedState != null && UpdatedState != CurrentState)
                {
                    StateChange(UpdatedState);
                }
            }
        }

        public void StateChange(State toState)
        {
            if (CurrentState != null) 
            {
                // Exit previous state // 
                Debugger.Instance.Log("State Exit: " + CurrentState.ToString());
                CurrentState.OnStateExit();
            } 

            CurrentState = toState; // Update current state to given state //

            Debugger.Instance.Log("State Enter: " + CurrentState.ToString());
            CurrentState.OnStateEnter(); // Process current state enter //
        }

        private void Reset()
        {
            // Attach first state object by default
            if(transform.GetChild(0).GetComponent<State>() != null)
            {
                DefaultState = transform.GetChild(0).GetComponent<State>();
            }
        }
    }
}