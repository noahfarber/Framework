using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class PlayingState : NestedStateManager
    {
        [SerializeField] private GameObject _Game;

        [Header("Transition States")]
        [SerializeField] private State _Paused;
        private State _StateBeforePause;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _Game.SetActive(true);
        }

        public override State OnUpdate()
        {
            State rtn = null;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }

            base.OnUpdate(); // Updated nested states

            return rtn;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _Game.SetActive(false);
        }

        public void PauseGame()
        {
            if (CurrentState != _Paused)
            {
                _StateBeforePause = CurrentState;
                StateChange(_Paused);
            }
            else if (CurrentState == _Paused)
            {
                if (_StateBeforePause != null) { StateChange(_StateBeforePause); }
                else { Debugger.Instance.LogError("Can't unpause... No previous state found."); }
            }
        }
    }
}