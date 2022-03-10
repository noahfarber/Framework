using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using UnityEngine.UI;

public class GameOverState : State
{
    [SerializeField] private Button _RestartButton;

    private void Start()
    {
        _RestartButton.gameObject.SetActive(false);
    }

    public override void OnStateEnter()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
