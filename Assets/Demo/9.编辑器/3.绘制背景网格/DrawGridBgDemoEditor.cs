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
        //�����קƫ����
        private Vector2 deltaVector = new Vector2();

        #region �򿪴���
        [MenuItem("���Ʊ�������", menuItem = "�Զ���༭����ʾ/3.���Ʊ�������")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<DrawGridBgDemoEditor>("���Ʊ�������");
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
        /// ��������
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="alpha"></param>
        private void DrawGrid(float gridSize, float alpha)
        {
            var windowWidth = position.width;
            var windowHeight = position.height;

            //����������ߣ���ֹ�ƶ�ʱ�߽���ʾ����
            int horizontialLineCount = (int)(windowHeight / gridSize) + 2;
            int verticalLineCount = (int)(windowWidth / gridSize) + 2;

            Handles.color = new Color(Color.white.r, Color.white.g, Color.white.b, alpha);

            //ƫ����
            var gridDelta = new Vector2(deltaVector.x % gridSize, deltaVector.y % gridSize);

            //����ˮƽ��
            for (int i = 0; i < horizontialLineCount; i++)
            {
                //��Ҫ����һ����Ԫ��ĳ��ȣ�����Ч���ȽϺ�
                Handles.DrawLine(new Vector3(-gridSize, gridSize * i + gridDelta.y), new Vector3(windowWidth + gridSize, gridSize * i + gridDelta.y));
            }

            //���ƴ�ֱ��
            for (int i = 0; i < verticalLineCount; i++)
            {
                Handles.DrawLine(new Vector3(gridSize * i + gridDelta.x, -gridSize), new Vector3(gridSize * i + gridDelta.x, windowHeight + gridSize));
            }
        }
    }
#endif
}