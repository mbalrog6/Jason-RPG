﻿using UnityEngine;

namespace JasonRPG.UI
{
    public class ExitButton : MonoBehaviour
    {
        public void ExitApplication()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

}