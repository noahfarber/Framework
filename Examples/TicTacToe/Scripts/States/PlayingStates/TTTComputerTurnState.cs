using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace TicTacToe
{
    public class TTTComputerTurnState : State
    {
        [SerializeField] private TicTacToe _Game;
        [SerializeField] private AudioClip _ComputerTurnSound;

        [Header("Transition States")]
        [SerializeField] private TTTPlayerTurnState _PlayerTurn;
        [SerializeField] private TTTGameOverState _GameOver;

        private float _ComputerTurnCurrentTime = 0f;
        private float _ComputerTurnDuration = 1f;
        private bool _MoveMade = false;

        public override void OnStateEnter()
        {
            _ComputerTurnCurrentTime = 0f;
            _ComputerTurnDuration = Random.Range(.25f, 1.25f);
            _MoveMade = false;
        }

        public override State OnUpdate()
        {
            State rtn = null;

            CheckForComputerInput();

            if (_MoveMade)
            {
                rtn = CheckExitConditions();
            }

            return rtn;
        }

        public override void OnStateExit()
        {

        }

        private void CheckForComputerInput()
        {
            if (_ComputerTurnCurrentTime >= _ComputerTurnDuration) // Wait for a second before making a move
            {
                _Game.UpdateBoard(_Game.ComputerStrategy(), _Game.ComputerTileID);
                _MoveMade = true;
            }
            else
            {
                _ComputerTurnCurrentTime += Time.deltaTime;
            }
        }

        private State CheckExitConditions()
        {
            State rtn = null;

            if (_Game.EvaluateWin(_Game.ComputerTileID))
            {
                _Game.OnGameOver(_Game.ComputerWinMessage);
                rtn = _GameOver;
            }
            else if (_Game.IsStalemate())
            {
                _Game.OnGameOver(_Game.StalemateMessage);
                rtn = _GameOver;
            }
            else
            {
                _Game.OnTurnOverMessage(_Game.PlayerTurnMessage);
                rtn = _PlayerTurn;
            }

            return rtn;
        }
    }
}