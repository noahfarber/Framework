using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Framework
{
    public class Debugger : MonoBehaviour
    {
        public static Debugger Instance;
        public TextMeshProUGUI ScreenLog;
        private string ScreenLogMsg = "";

        public bool ShowOnScreen = false;
        public bool Logs = true;
        public bool Erorrs = true;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Log(string message)
        {
            if (Logs)
            {
                Debug.Log(message);
                AddToInternalLog(message);
            }
        }

        public void LogError(string message)
        {
            if (Erorrs)
            {
                Debug.LogError(message);
                AddToInternalLog(message);
            }
        }

        private void AddToInternalLog(string message)
        {
            ScreenLogMsg += message + System.Environment.NewLine;

            if (ShowOnScreen)
            {
                ScreenLog.text = ScreenLogMsg;
            }
        }
    }

}