using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace TicTacToe
{
    public class TTTPlayingState : NestedStateManager
    {
        [SerializeField] private TicTacToe _Game;
        [SerializeField] private AudioSource _GameMusic;

        [Header("Transition States")]
        [SerializeField] private TTTPausedState _Paused;
        private State _StateBeforePause;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SoundManager.Instance.PlayAndFade(_GameMusic, 1f, .5f);
            _Game.gameObject.SetActive(true);
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
            _Game.gameObject.SetActive(false);
            SoundManager.Instance.Stop(_GameMusic);
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