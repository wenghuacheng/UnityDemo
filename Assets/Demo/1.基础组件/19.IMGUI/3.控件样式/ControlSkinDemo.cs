using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    /// <summary>
    /// �ؼ�Ƥ��
    /// ����һ��GUI Skin�ļ���������
    /// </summary>
    public class ControlSkinDemo : MonoBehaviour
    {
        public GUISkin skin;

        void OnGUI()
        {
            GUI.skin = skin;
            GUILayout.Button("I am a re-Skinned Button");//ʹ��Ƥ��
        }
    }
}