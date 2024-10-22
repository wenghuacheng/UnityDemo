using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.CustomEditors.ToolKit
{
#if UNITY_EDITOR
    public class DemoShowGridToolKitEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/3.网格背景")]
        public static void OpenWindow()
        {
            DemoShowGridToolKitEditor wnd = GetWindow<DemoShowGridToolKitEditor>();
            wnd.titleContent = new GUIContent("网格背景");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            //加载布局文件
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Demo/6.1编辑器(uiToolkit)/3.1.GraphView网格/UI/DemoGridToolKitEditor.uxml");
            visualTree.CloneTree(root);

            //加载样式文件
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Demo/6.1编辑器(uiToolkit)/3.1.GraphView网格/UI/DemoGridToolKitEditor.uss");
            root.styleSheets.Add(styleSheet);
        }
    }

#endif
}
