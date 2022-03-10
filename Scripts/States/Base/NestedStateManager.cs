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

        public override void OnStateEnter()
        {
            if (DefaultState != null) { StateChange(DefaultState); }
        }

        public override void OnUpdate()
        {
            ProcessStates();
        }

        public override void OnStateExit()
        {

        }

        public virtual void ProcessStates()
        {
            if (CurrentState != null)
            {
                CurrentState.OnUpdate();
            }
        }

        public virtual void StateChange(State toState)
        {
            if (CurrentState != null) { CurrentState.OnStateExit(); } // Exit previous state //
            CurrentState = toState; // Update current state to given state //
            CurrentState.OnStateEnter(); // Process current state enter //
        }
    }
}