using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace TicTacToe
{
    public class TTTInitGameState : State
    {
        [SerializeField] private TicTacToe _Game;

        [Header("Transition States")]
        [SerializeField] private TTTPlayerTurnState _PlayerTurn;

        public override void OnStateEnter()
        {
            _Game.Init();
        }

        public override State OnUpdate()
        {
            return _PlayerTurn;
        }

        public override void OnStateExit()
        {

        }
    }

}