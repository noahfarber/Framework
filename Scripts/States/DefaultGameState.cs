using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class DefaultGameState : State
    {
        public override void OnStateEnter()
        {

        }

        public override State OnUpdate()
        {
            State rtn = null;
            return rtn;
        }

        public override void OnStateExit()
        {

        }
    }
}