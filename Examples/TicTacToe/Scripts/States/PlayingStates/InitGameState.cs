using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class InitGameState : State
{
    public TicTacToeGameController Controller;
    [SerializeField] private TicTacToe Game;

    public override void OnStateEnter()
    {
        SoundManager.Instance.Fade(Controller.SystemSoundData.MenuMusic, 0f, .5f);
        SoundManager.Instance.PlayAndFade(Controller.SystemSoundData.GameMusic, 1f, .5f);

        Controller.TicTacToeGame.gameObject.SetActive(true);
        Game.Build();
        Game.Init();

    }

    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    {

    }
}
