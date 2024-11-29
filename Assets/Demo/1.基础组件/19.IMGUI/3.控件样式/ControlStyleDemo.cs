using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    /// <summary>
    /// ÑשÊ½
    /// </summary>
    public class ControlStyleDemo : MonoBehaviour
    {
        public GUIStyle style;

        void OnGUI()
        {
            GUILayout.Button("I am a custom-styled Button", style);
        }
    }
}