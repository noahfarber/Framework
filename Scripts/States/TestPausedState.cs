using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPausedState : State
{
    [SerializeField] private GameObject PauseMenu;

    public override void OnStateEnter()
    {
        PauseMenu.SetActive(false);
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateExit()
    {
        PauseMenu.SetActive(false);
    }
}
