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
                ScreenLogMsg += message + System.Environment.NewLine;
                ScreenLog.text = ScreenLogMsg;
            }
        }

        public void LogError(string message)
        {
            if (Erorrs)
            {
                Debug.LogError(message);
                ScreenLogMsg += message + System.Environment.NewLine;
                ScreenLog.text = ScreenLogMsg;
            }
        }
    }

}