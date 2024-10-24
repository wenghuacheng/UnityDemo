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
    public class DemoCreateConnectNodeToolKitEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/6.创建节点连线")]
        public static void OpenWindow()
        {
            DemoCreateConnectNodeToolKitEditor wnd = GetWindow<DemoCreateConnectNodeToolKitEditor>();
            wnd.titleContent = new GUIContent("创建节点连线");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            //加载布局文件
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Demo/6.1编辑器(uiToolkit)/3.4.GraphView连线/DemoCreateConnectNodeToolKitGraphView.uxml");
            visualTree.CloneTree(root);
        }
    }

    

#endif
}
