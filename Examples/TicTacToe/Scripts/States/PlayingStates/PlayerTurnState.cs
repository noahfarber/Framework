using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class PlayerTurnState : State
{
    [SerializeField] private TicTacToe Game;
    [SerializeField] private ComputerTurnState ComputerTurn;
    [SerializeField] private GameOverState GameOver;
    [SerializeField] private PausedState Paused;

    public override void OnStateEnter()
    {
        Game.PlayerTilePressed += TilePressed;
    }

    public override State OnUpdate()
    {
        State rtn = null;
        State exitCheck = CheckExitConditions();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rtn = Paused;
        }
        if(exitCheck != null)
        {
            rtn = exitCheck;
        }

        return rtn;
    }

    public override void OnStateExit()
    {
        Game.PlayerTilePressed -= TilePressed;
    }

    
    private void TilePressed(int selection)
    {
        Game.CheckTilePressed(selection);
    }

    private State CheckExitConditions()
    {
        State rtn = null;

        if (Game.EvaluateWin(Game.PlayerTileID))
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
            Game.OnTurnOverMessage(Game.ComputerTurnMessage);
            rtn = ComputerTurn;
        }

        return rtn;
    }
}
