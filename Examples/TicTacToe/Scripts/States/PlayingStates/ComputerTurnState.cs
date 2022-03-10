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
        if (_ComputerTurnCurrentTime >= _ComputerTurnDuration) // Wait for a second before making a move
        {
            int selection = Game.ComputerStrategy();

            Game.UpdateBoard(selection, Game.ComputerTileID);

            SoundManager.Instance.PlayOneShot(Game.GameSoundData.ComputerTurn, 1f);

            if (Game.EvaluateWin(Game.ComputerTileID))
            {
                Game.OnGameOver(Game.ComputerWinMessage);
            }
            else if (Game.IsStalemate())
            {
                rtn = GameOver;
                Game.OnGameOver(Game.StalemateMessage);
            }
            else
            {
                rtn = PlayerTurn;
                Game.OnTurnOverMessage(Game.PlayerTurnMessage);
            }
        }
        else
        {
            _ComputerTurnCurrentTime += Time.deltaTime;
        }

        return rtn;
    }

    public override void OnStateExit()
    {

    }

    public void Input()
    {

    }
}
