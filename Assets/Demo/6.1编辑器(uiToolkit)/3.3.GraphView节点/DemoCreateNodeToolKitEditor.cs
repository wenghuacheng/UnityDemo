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
    public class DemoCreateNodeToolKitEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/5.创建节点")]
        public static void OpenWindow()
        {
            DemoCreateNodeToolKitEditor wnd = GetWindow<DemoCreateNodeToolKitEditor>();
            wnd.titleContent = new GUIContent("创建节点");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            //加载布局文件
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Demo/6.1编辑器(uiToolkit)/3.3.GraphView节点/DemoCreateNodeToolKitGraphView.uxml");
            visualTree.CloneTree(root);

        }
    }

#endif
}
