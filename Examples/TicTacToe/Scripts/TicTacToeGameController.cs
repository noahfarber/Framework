using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Framework;

public class TicTacToeGameController : GameController
{
    public TicTacToe TicTacToeGame;
    public SystemSounds SystemSoundData;

    public override void Start()
    {
        base.Start();
        TicTacToeGame.Build();
    }

    public override void StartGame()
    {
        SoundManager.Instance.Fade(SystemSoundData.MenuMusic, 0f, .5f);

        TicTacToeGame.gameObject.SetActive(true);
        TicTacToeGame.Init();

        base.StartGame();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (IsPlayingGame())
        {
            TicTacToeGame.UpdateGame();
        }

        base.Update();
    }

    public override void ReturnToMenu()
    {
        TicTacToeGame.gameObject.SetActive(false);

        SoundManager.Instance.PlayAndFade(SystemSoundData.MenuMusic, 1f, .25f, 0f);
        SoundManager.Instance.Stop(SystemSoundData.GameMusic);
        SoundManager.Instance.Stop(SystemSoundData.PauseMusic);

        base.ReturnToMenu();
    }

    public override void PauseGame()
    {
        SoundManager.Instance.Fade(SystemSoundData.GameMusic, 0f, .1f);
        SoundManager.Instance.PlayAndFade(SystemSoundData.PauseMusic, .5f, .1f, 0f);
        base.PauseGame();
    }

    public override void UnpauseGame()
    {
        SoundManager.Instance.Fade(SystemSoundData.GameMusic, 1f, .25f);
        SoundManager.Instance.Fade(SystemSoundData.PauseMusic, 0f, 1f);
        base.UnpauseGame();
    }

}

