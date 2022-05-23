using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;

namespace TicTacToe
{
    public class TTTMainMenuState : State
    {
        [SerializeField] private GameObject _MainMenu;
        [SerializeField] private GameObject _PauseMenu;
        [SerializeField] private AudioSource _MenuMusic;

        [Header("Transition States")]
        [SerializeField] private TTTPlayingState _Playing;

        private bool _StartGameRequested = false;

        public override void OnStateEnter()
        {
            _StartGameRequested = false;
            _MainMenu.SetActive(true);
            _PauseMenu.SetActive(false);
            SoundController.Instance.PlayAndFade(_MenuMusic, 1f, .25f, 0f);
        }

        public override State OnUpdate()
        {
            State rtn = null;

            if (_StartGameRequested)
            {
                rtn = _Playing;
            }

            return rtn;
        }

        public override void OnStateExit()
        {
            _MainMenu.SetActive(false);
            _PauseMenu.SetActive(false);
            SoundController.Instance.Fade(_MenuMusic, 0f, .5f);
        }

        public void RequestStartGame()
        {
            _StartGameRequested = true;
        }
    }
}