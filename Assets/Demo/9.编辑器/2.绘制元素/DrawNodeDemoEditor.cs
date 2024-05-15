using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// 绘制元素示例
    /// </summary>
#if UNITY_EDITOR
    public class DrawNodeDemoEditor : EditorWindow
    {
        //元素样式
        private GUIStyle nodeStyle;
        //元素列表
        private List<NodeInfo> nodeList = new List<NodeInfo>();

        #region 打开窗体

        [MenuItem("绘制元素", menuItem = "自定义编辑器演示/2.绘制元素")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<DrawNodeDemoEditor>("绘制元素");
        }
#endregion

        private void OnEnable()
        {
            nodeStyle = new GUIStyle();
            //加载系统默认的样式
            nodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(10, 10, 10, 10);
        }

        /// <summary>
        /// 元素绘制与事件响应
        /// </summary>
        private void OnGUI()
        {
            //事件响应
            ProcessEvent();

            //绘制方法需要不停的被调用，否则不会被渲染
            DrawNodes();
        }

        #region 事件响应
        private void ProcessEvent()
        {
            var @event = Event.current;

            if (@event.type == EventType.MouseDown && @event.button == 0)
            {
                //在编辑器页面上点击左键生成节点
                NodeInfo node = new NodeInfo(@event.mousePosition);
                nodeList.Add(node);
            }
        }
        #endregion

        #region 绘制

        /// <summary>
        /// 绘制节点
        /// </summary>
        private void DrawNodes()
        {
            foreach (var node in nodeList)
            {
                node.Draw(nodeStyle);
            }
        }
        #endregion

        #region Inner Class
        /// <summary>
        /// 节点对象
        /// </summary>
        private class NodeInfo : ScriptableObject
        {
            private Rect rect;
            private const int nodeWidth = 150;
            private const int nodeHeight = 75;

            public NodeInfo(Vector2 position)
            {
                rect = new Rect(position.x - nodeWidth / 2, position.y - nodeHeight / 2, nodeWidth, nodeHeight);
            }

            public void Draw(GUIStyle nodeStyle)
            {
                GUILayout.BeginArea(rect, nodeStyle);
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.LabelField("123456");

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(this);
                }

                GUILayout.EndArea();
            }
        }

        #endregion
    }
#endif
}