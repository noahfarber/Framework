using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace Framework
{
    public class NestedStateManager : State
    {
        [SerializeField] private State DefaultState = null;
        [HideInInspector] public State CurrentState = null;
        private bool CanExit = false;

        #region State Functions
        public override void OnStateEnter()
        {
            if (DefaultState != null) { StateChange(DefaultState); }
            CanExit = false;
        }

        public override State OnUpdate()
        {
            ProcessStates();
            return CurrentState;
        }

        public override void OnStateExit()
        {
            CurrentState.OnStateExit(); // Call exit for current state since leaving this state inherently means leaving the substate as well.
        }
        #endregion

        #region State Machine Functions
        public void ProcessStates()
        {
            if (CurrentState != null)
            {
                State UpdatedState = CurrentState.OnUpdate();

                if (UpdatedState != null && UpdatedState != CurrentState)
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
        #endregion
    }
}