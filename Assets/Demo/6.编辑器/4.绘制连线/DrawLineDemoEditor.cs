using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
#if UNITY_EDITOR
    public class DrawLineDemoEditor : EditorWindow
    {
        private List<LineInfo> lines = new List<LineInfo>();

        //开始拖拽
        private bool IsDragging;
        //连线的起点
        private Vector2 startLinePosition;
        private Vector2 endLinePosition;

        #region 打开窗体
        [MenuItem("绘制连线", menuItem = "自定义编辑器演示/4.绘制连线")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<DrawLineDemoEditor>("绘制连线");
        }
        #endregion

        private void OnGUI()
        {
            ProcessEvent();
            DrawConnectionLine();
            DrawLines();

            if (GUI.changed)
                Repaint();
        }

        private void ProcessEvent()
        {
            Event @event = Event.current;

            Debug.Log(@event.type);

            if (@event.type == EventType.MouseDown && @event.button == 1 && !IsDragging)
            {
                //右键弹出菜单
                //ShowContextMenu(@event.mousePosition);
                //需要鼠标一直点击才能触发，否则不会响应
                StartConnect(@event.mousePosition);
            }
            else if (@event.type == EventType.MouseDrag && IsDragging)
            {
                //连线拖拽             
                endLinePosition = @event.mousePosition;
                GUI.changed = true;
            }
            else if (@event.type == EventType.MouseUp && @event.button == 1 && IsDragging)
            {
                //结束拖拽
                EndConnect(@event.mousePosition);
            }
        }

        #region 右键菜单
        [Obsolete("还是需要用户保持点击状态才可以响应事件")]
        private void ShowContextMenu(Vector2 mousePosition)
        {
            GenericMenu menu = new GenericMenu();
            //添加菜单项并设置处理函数
            menu.AddItem(new GUIContent("开始连线"), false, StartConnect, mousePosition);
            menu.ShowAsContext();
        }

        #endregion

        /// <summary>
        /// 开始连线
        /// </summary>
        private void StartConnect(object mousePositionObj)
        {
            var mousePosition = (Vector2)mousePositionObj;
            this.startLinePosition = mousePosition;
            IsDragging = true;
        }

        /// <summary>
        /// 结束连线
        /// </summary>
        private void EndConnect(Vector2 endPosition)
        {
            IsDragging = false;
            LineInfo line = new LineInfo(startLinePosition, endPosition);
            lines.Add(line);
        }

        /// <summary>
        /// 绘制正在连接的线
        /// </summary>
        private void DrawConnectionLine()
        {
            if (!IsDragging) return;

            Vector2 startPoint = startLinePosition;
            Vector2 endPoint = endLinePosition;
            Handles.DrawBezier(startPoint, endPoint, startPoint, endPoint, Color.white, null, 3);
        }

        /// <summary>
        /// 绘制已连接的线
        /// </summary>
        private void DrawLines()
        {
            foreach (var line in lines)
            {
                line.Draw();
            }
        }

        #region Inner Class
        public class LineInfo : ScriptableObject
        {
            private float width = 5;

            public LineInfo(Vector2 startPoint, Vector2 endPoint)
            {
                this.startPoint = startPoint;
                this.endPoint = endPoint;
            }

            public Vector2 startPoint;
            public Vector2 endPoint;

            public void Draw()
            {
                Handles.DrawBezier(startPoint, endPoint, startPoint, endPoint, Color.white, null, width);
            }
        }
        #endregion

    }
#endif
}