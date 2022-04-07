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
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
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