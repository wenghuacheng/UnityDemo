using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.IMGUI
{
    //�༭���ؼ�
    public class ExampleWindow : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        [MenuItem("�Զ���༭��/IMGUI�༭���ؼ���ʾ")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ExampleWindow));
        }

        void OnGUI()
        {
            // �˴�Ϊʵ�ʴ��ڴ���

            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
        }

    }
}
