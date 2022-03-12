using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace TicTacToe
{
    public class TTTPlayerTurnState : State
    {
        [SerializeField] private TicTacToe Game;

        [Header("Transition States")]
        [SerializeField] private TTTComputerTurnState ComputerTurn;
        [SerializeField] private TTTGameOverState GameOver;

        private bool _MoveMade = false;

        public override void OnStateEnter()
        {
            _MoveMade = false;
            Game.PlayerTilePressed += TilePressed;
        }

        public override State OnUpdate()
        {
            State rtn = null;

            if (_MoveMade)
            {
                rtn = CheckExitConditions();
            }

            return rtn;
        }

        public override void OnStateExit()
        {
            Game.PlayerTilePressed -= TilePressed;
        }


        private void TilePressed(int selection)
        {
            _MoveMade = Game.CheckTilePressed(selection);
        }

        private State CheckExitConditions()
        {
            State rtn = null;

            if (Game.EvaluateWin(Game.PlayerTileID))
            {
                Game.OnGameOver(Game.PlayerWinMessage);
                rtn = GameOver;
            }
            else if (Game.IsStalemate())
            {
                Game.OnGameOver(Game.StalemateMessage);
                rtn = GameOver;
            }
            else
            {
                Game.OnTurnOverMessage(Game.ComputerTurnMessage);
                rtn = ComputerTurn;
            }

            return rtn;
        }
    }
}