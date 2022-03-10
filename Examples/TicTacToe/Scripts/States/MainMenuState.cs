using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;

public class MainMenuState : State
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Button PlayButton;
    public AudioSource MenuMusic;

    public override void OnStateEnter()
    {
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        SoundManager.Instance.PlayAndFade(MenuMusic, 1f, .25f, 0f);
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateExit()
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }
}
