using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class State : MonoBehaviour
    {
        public abstract void OnStateEnter();
        public abstract void OnUpdate();
        public abstract void OnStateExit();
    }
}