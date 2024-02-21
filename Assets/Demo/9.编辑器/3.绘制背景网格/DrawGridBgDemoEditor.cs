using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
#if UNITY_EDITOR
    public class DrawGridBgDemoEditor : EditorWindow
    {
        //鼠标拖拽偏移量
        private Vector2 deltaVector = new Vector2();

        #region 打开窗体
        [MenuItem("绘制背景网格", menuItem = "自定义编辑器演示/3.绘制背景网格")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<DrawGridBgDemoEditor>("绘制背景网格");
        }
        #endregion

        private void OnGUI()
        {
            ProcessEvent();

            DrawGrid(25, 0.3f);
            DrawGrid(100, 0.1f);

            if (GUI.changed)
                Repaint();
        }

        private void ProcessEvent()
        {
            var @event = Event.current;
            if (@event.type == EventType.MouseDrag && @event.button == 0)
            {
                deltaVector += @event.delta;
                GUI.changed = true;
            }
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="alpha"></param>
        private void DrawGrid(float gridSize, float alpha)
        {
            var windowWidth = position.width;
            var windowHeight = position.height;

            //多绘制两条线，防止移动时边界显示问题
            int horizontialLineCount = (int)(windowHeight / gridSize) + 2;
            int verticalLineCount = (int)(windowWidth / gridSize) + 2;

            Handles.color = new Color(Color.white.r, Color.white.g, Color.white.b, alpha);

            //偏移量
            var gridDelta = new Vector2(deltaVector.x % gridSize, deltaVector.y % gridSize);

            //绘制水平线
            for (int i = 0; i < horizontialLineCount; i++)
            {
                //需要向外一个单元格的长度，这样效果比较好
                Handles.DrawLine(new Vector3(-gridSize, gridSize * i + gridDelta.y), new Vector3(windowWidth + gridSize, gridSize * i + gridDelta.y));
            }

            //绘制垂直线
            for (int i = 0; i < verticalLineCount; i++)
            {
                Handles.DrawLine(new Vector3(gridSize * i + gridDelta.x, -gridSize), new Vector3(gridSize * i + gridDelta.x, windowHeight + gridSize));
            }
        }
    }
#endif
}