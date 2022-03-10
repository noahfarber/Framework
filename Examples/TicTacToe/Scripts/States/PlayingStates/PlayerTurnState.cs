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

    private State NextState = null;

    public override void OnStateEnter()
    {
        Game.PlayerTilePressed += TilePressed;
        NextState = null;
    }

    public override State OnUpdate()
    {
        State rtn = null;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            rtn = Paused;
        }
        if(NextState != null) { rtn = NextState; }
        return rtn;
    }

    public override void OnStateExit()
    {
        Game.PlayerTilePressed -= TilePressed;
    }

    
    private void TilePressed(int selection)
    {
        // If valid input
        if (_GameBoard[selection] == EmptyTileID)
        {
            UpdateBoard(selection, PlayerTileID);

            SoundManager.Instance.PlayOneShot(GameSoundData.ClickedTile, 1f);

            if (EvaluateWin(PlayerTileID))
            {
                Game.OnGameOver(Game.ComputerWinMessage);
            }
            else if (Game.IsStalemate())
            {
                Game.OnGameOver(Game.StalemateMessage);
                NextState = GameOver;
            }
            else
            {
                Game.GameMessageText.text = Game.ComputerTurnMessage;
            }
        }

    }
}
