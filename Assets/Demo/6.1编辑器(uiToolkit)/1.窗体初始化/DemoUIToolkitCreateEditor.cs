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
    public class DemoUIToolkitCreateEditor : EditorWindow
    {
        [MenuItem("UIToolKit编辑器/1.启动窗体")]
        public static void OpenWindow()
        {
            DemoUIToolkitCreateEditor wnd = GetWindow<DemoUIToolkitCreateEditor>();
            wnd.titleContent = new GUIContent("窗体启动演示");
        }

        /// <summary>
        /// 打开窗体时触发（针对UI toolkit组件）
        /// 该方法会被自动调用
        /// </summary>
        public void CreateGUI()
        {
            //方式一
            LoadControl();

            //方式二
            //LoadAsset();
        }

        /// <summary>
        /// 通过加载本地布局与样式表方式
        /// </summary>
        private void LoadAsset()
        {
            VisualElement root = rootVisualElement;

            //加载布局文件
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);

            //加载样式文件
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            ////PS:初始化时获取需要的控件
            //root.Q();
        }

        /// <summary>
        /// 通过自定义控件
        /// </summary>
        private void LoadControl()
        {
            // 创建一个简单的 UI
            Label label = new Label("Hello World");
            Button button = new Button(() => Debug.Log("Button Clicked!")) { text = "Click Me!" };

            // 将 UI 元素添加到根视觉元素
            VisualElement root = rootVisualElement;
            root.Add(label);
            root.Add(button);
        }
    }

#endif
}
