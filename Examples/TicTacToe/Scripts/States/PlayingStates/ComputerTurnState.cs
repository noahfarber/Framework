using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class ComputerTurnState : State
{
    [SerializeField] private TicTacToe Game;
    private float _ComputerTurnCurrentTime = 0f;
    private float _ComputerTurnDuration = 1f;
    [SerializeField] private AudioClip ComputerTurnSound;
    [Space(30)]
    [SerializeField] private PlayerTurnState PlayerTurn;
    [SerializeField] private GameOverState GameOver;

    public override void OnStateEnter()
    {
        _ComputerTurnCurrentTime = 0f;
        _ComputerTurnDuration = Random.Range(.25f, 1.25f);
    }

    public override State OnUpdate()
    {
        State rtn = null;
        State exitCheck = null;

        CheckForComputerInput();

        exitCheck = CheckExitConditions();
        if(exitCheck != null)
        {
            rtn = exitCheck;
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
            Game.UpdateBoard(Game.ComputerStrategy(), Game.ComputerTileID);
        }
        else
        {
            _ComputerTurnCurrentTime += Time.deltaTime;
        }
    }

    private State CheckExitConditions()
    {
        State rtn = null;

        if (Game.EvaluateWin(Game.ComputerTileID))
        {
            Game.OnGameOver(Game.ComputerWinMessage);
            rtn = GameOver;
        }
        else if (Game.IsStalemate())
        {
            Game.OnGameOver(Game.StalemateMessage);
            rtn = GameOver;
        }
        else
        {
            Game.OnTurnOverMessage(Game.PlayerTurnMessage);
            rtn = PlayerTurn;
        }

        return rtn;
    }
}
