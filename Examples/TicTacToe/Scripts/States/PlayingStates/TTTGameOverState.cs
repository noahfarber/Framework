using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using UnityEngine.UI;

namespace TicTacToe
{
    public class TTTGameOverState : State
    {
        [SerializeField] private Button _RestartButton;

        [Header("Transition States")]
        [SerializeField] private TTTInitGameState InitState;

        private bool _ExecuteRestart = false;

        private void Start()
        {
            _RestartButton.gameObject.SetActive(false);
        }

        public override void OnStateEnter()
        {
            _RestartButton.gameObject.SetActive(true);
            _ExecuteRestart = false;
        }

        public override State OnUpdate()
        {
            State rtn = null;

            if (_ExecuteRestart)
            {
                rtn = InitState;
            }

            return rtn;
        }

        public override void OnStateExit()
        {
            _RestartButton.gameObject.SetActive(false);
        }

        public void RestartGamePressed()
        {
            _ExecuteRestart = true;
        }
    }

}