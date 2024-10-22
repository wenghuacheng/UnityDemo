using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
#if UNITY_EDITOR
    public class DemoContextMenuToolKitEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/4.右键菜单")]
        public static void OpenWindow()
        {
            DemoContextMenuToolKitEditor wnd = GetWindow<DemoContextMenuToolKitEditor>();
            wnd.titleContent = new GUIContent("右键菜单");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            //加载布局文件
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Demo/6.1编辑器(uiToolkit)/3.2.GraphView右键菜单/DemoContextMenuToolKitEditor.uxml");
            visualTree.CloneTree(root);
        }
    }

#endif
}
