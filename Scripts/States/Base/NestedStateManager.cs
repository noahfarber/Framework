using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace Framework
{
    public class NestedStateManager : State
    {
        [SerializeField] private State DefaultState;
        [HideInInspector] public State CurrentState;
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

        }

        #region State Machine Functions
        public void ProcessStates()
        {
            if (CurrentState != null)
            {
                State UpdatedState = CurrentState.OnUpdate();

                if (UpdatedState != CurrentState)
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
        #endregion
    }
}