using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class GameController : MonoBehaviour
    {
        public StateManager StateManager;

        public virtual void Start()
        {
            StateManager.Init();
        }

        public virtual void Update()
        {
            StateManager.ProcessStates();
        }

        public virtual void ExitApplication()
        {
            Application.Quit();
        }

        private void Reset()
        {
            // Attach default references if applicable
            if(GetComponent<StateManager>() != null)
            {
                StateManager = GetComponent<StateManager>();
            }
        }
    }
}