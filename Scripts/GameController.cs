using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class GameController : MonoBehaviour
    {
        public StateManager StateManager;

        [Header("Transition States")]
        private State _StateBeforePause;
        [SerializeField] private State _Paused;

        public virtual void Start()
        {
            StateManager.Init();
        }

        public virtual void Update()
        {
            StateManager.ProcessStates();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            if (StateManager.CurrentState != _Paused)
            {
                _StateBeforePause = StateManager.CurrentState;
                StateManager.StateChange(_Paused);
            }
            else if (StateManager.CurrentState == _Paused)
            {
                if (_StateBeforePause != null) { StateManager.StateChange(_StateBeforePause); }
                else { Debugger.Instance.LogError("Can't unpause... No previous state found."); }
            }
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