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

        //��ʼ��ק
        private bool IsDragging;
        //���ߵ����
        private Vector2 startLinePosition;
        private Vector2 endLinePosition;

        #region �򿪴���
        [MenuItem("��������", menuItem = "�Զ���༭����ʾ/4.��������")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<DrawLineDemoEditor>("��������");
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
                //�Ҽ������˵�
                //ShowContextMenu(@event.mousePosition);
                //��Ҫ���һֱ������ܴ��������򲻻���Ӧ
                StartConnect(@event.mousePosition);
            }
            else if (@event.type == EventType.MouseDrag && IsDragging)
            {
                //������ק             
                endLinePosition = @event.mousePosition;
                GUI.changed = true;
            }
            else if (@event.type == EventType.MouseUp && @event.button == 1 && IsDragging)
            {
                //������ק
                EndConnect(@event.mousePosition);
            }
        }

        #region �Ҽ��˵�
        [Obsolete("������Ҫ�û����ֵ��״̬�ſ�����Ӧ�¼�")]
        private void ShowContextMenu(Vector2 mousePosition)
        {
            GenericMenu menu = new GenericMenu();
            //��Ӳ˵�����ô�����
            menu.AddItem(new GUIContent("��ʼ����"), false, StartConnect, mousePosition);
            menu.ShowAsContext();
        }

        #endregion

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void StartConnect(object mousePositionObj)
        {
            var mousePosition = (Vector2)mousePositionObj;
            this.startLinePosition = mousePosition;
            IsDragging = true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void EndConnect(Vector2 endPosition)
        {
            IsDragging = false;
            LineInfo line = new LineInfo(startLinePosition, endPosition);
            lines.Add(line);
        }

        /// <summary>
        /// �����������ӵ���
        /// </summary>
        private void DrawConnectionLine()
        {
            if (!IsDragging) return;

            Vector2 startPoint = startLinePosition;
            Vector2 endPoint = endLinePosition;
            Handles.DrawBezier(startPoint, endPoint, startPoint, endPoint, Color.white, null, 3);
        }

        /// <summary>
        /// ���������ӵ���
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