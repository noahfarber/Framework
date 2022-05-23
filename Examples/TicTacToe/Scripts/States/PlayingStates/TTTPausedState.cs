using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace TicTacToe
{
    public class TTTPausedState : State
    {
        [SerializeField] private GameObject _PauseMenu;
        [SerializeField] private AudioSource _PauseMusic;
        [SerializeField] private AudioSource _GameMusic;

        public override void OnStateEnter()
        {
            _PauseMenu.SetActive(true);
            SoundController.Instance.Fade(_GameMusic, 0f, .1f);
            SoundController.Instance.PlayAndFade(_PauseMusic, .5f, .1f, 0f);
        }

        public override State OnUpdate()
        {
            State rtn = null;
            return rtn;
        }

        public override void OnStateExit()
        {
            _PauseMenu.SetActive(false);
            SoundController.Instance.Fade(_GameMusic, 1f, .25f);
            SoundController.Instance.Fade(_PauseMusic, 0f, 1f);
        }
    }

}