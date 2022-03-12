using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class PausedState : State
    {
        [SerializeField] private GameObject PauseMenu;

        public override void OnStateEnter()
        {
            PauseMenu.SetActive(true);
        }

        public override State OnUpdate()
        {
            State rtn = null;
            return rtn;
        }

        public override void OnStateExit()
        {
            PauseMenu.SetActive(false);
        }
    }
}
