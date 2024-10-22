using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
#if UNITY_EDITOR
    public class DemoUIToolkitSelectionFileEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/2.文件选择")]
        public static void OpenWindow()
        {
            DemoUIToolkitSelectionFileEditor wnd = GetWindow<DemoUIToolkitSelectionFileEditor>();
            wnd.titleContent = new GUIContent("窗体文件选择");
        }

        /// <summary>
        /// 文件选择
        /// 当选择文件时会被系统调用
        /// </summary>
        private void OnSelectionChange()
        {
            DemoTreeSO tree = Selection.activeObject as DemoTreeSO;
            if (tree)
            {
                //打开窗体后才会被触发
                Debug.Log("选择文件了");

                //PS:可以在CreateGUI中获取控件，然后在选择了文件后刷新编辑器页面
            }
        }
    }
#endif
}
