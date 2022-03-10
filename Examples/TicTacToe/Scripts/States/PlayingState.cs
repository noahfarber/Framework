using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class PlayingState : NestedStateManager
{
    public AudioSource GameMusic;

    public override void OnStateEnter()
    {
        SoundManager.Instance.PlayAndFade(GameMusic, 1f, .5f);
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
