using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuState : State
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Button PlayButton;

    public override void OnStateEnter()
    {
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
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
