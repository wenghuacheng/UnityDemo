using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.IMGUI
{
    //编辑器控件
    public class ExampleWindow : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        [MenuItem("自定义编辑器/IMGUI编辑器控件演示")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ExampleWindow));
        }

        void OnGUI()
        {
            // 此处为实际窗口代码

            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
        }

    }
}
