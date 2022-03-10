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

    public override void OnStateEnter()
    {
        _ComputerTurnCurrentTime = 0f;
        _ComputerTurnDuration = Random.Range(.25f, 1.25f);
    }

    public override void OnUpdate()
    {
        if (_ComputerTurnCurrentTime >= _ComputerTurnDuration)
        {
            Input();
        }
        else
        {
            _ComputerTurnCurrentTime += Time.deltaTime;
        }
    }

    public override void OnStateExit()
    {

    }

    public void Input()
    {
        int selection = Game.ComputerStrategy();

        Game.UpdateBoard(selection, Game.ComputerTileID);

        SoundManager.Instance.PlayOneShot(GameSoundData.ComputerTurn, 1f);

        if (Game.EvaluateWin(Game.ComputerTileID))
        {
            _ComputerScore++;
            OnGameOver(_ComputerWinMessage);
        }
        else if (Stalemate())
        {
            OnGameOver(_StalemateMessage);
        }
        else
        {
            _GameMessageText.text = _PlayerTurnMessage;
        }
    }
}
