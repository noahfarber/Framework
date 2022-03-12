using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class MainMenuState : State
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject PauseMenu;
        [SerializeField] private Button PlayButton;

        [Header("Transition States")]
        [SerializeField] private State _Playing;

        private bool _StartGameRequested = false;

        public override void OnStateEnter()
        {
            _StartGameRequested = false;
            MainMenu.SetActive(true);
            PauseMenu.SetActive(false);
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
            MainMenu.SetActive(false);
            PauseMenu.SetActive(false);
        }

        public void RequestStartGame()
        {
            _StartGameRequested = true;
        }
    }
}