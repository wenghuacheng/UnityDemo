using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    /// <summary>
    /// 控件皮肤
    /// 创建一个GUI Skin文件进行配置
    /// </summary>
    public class ControlSkinDemo : MonoBehaviour
    {
        public GUISkin skin;

        void OnGUI()
        {
            GUI.skin = skin;
            GUILayout.Button("I am a re-Skinned Button");//使用皮肤
        }
    }
}