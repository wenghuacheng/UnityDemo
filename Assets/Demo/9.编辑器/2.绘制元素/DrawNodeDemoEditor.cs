using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// ����Ԫ��ʾ��
    /// </summary>
#if UNITY_EDITOR
    public class DrawNodeDemoEditor : EditorWindow
    {
        //Ԫ����ʽ
        private GUIStyle nodeStyle;
        //Ԫ���б�
        private List<NodeInfo> nodeList = new List<NodeInfo>();

        #region �򿪴���

        [MenuItem("����Ԫ��", menuItem = "�Զ���༭����ʾ/2.����Ԫ��")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<DrawNodeDemoEditor>("����Ԫ��");
        }
#endregion

        private void OnEnable()
        {
            nodeStyle = new GUIStyle();
            //����ϵͳĬ�ϵ���ʽ
            nodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(10, 10, 10, 10);
        }

        /// <summary>
        /// Ԫ�ػ������¼���Ӧ
        /// </summary>
        private void OnGUI()
        {
            //�¼���Ӧ
            ProcessEvent();

            //���Ʒ�����Ҫ��ͣ�ı����ã����򲻻ᱻ��Ⱦ
            DrawNodes();
        }

        #region �¼���Ӧ
        private void ProcessEvent()
        {
            var @event = Event.current;

            if (@event.type == EventType.MouseDown && @event.button == 0)
            {
                //�ڱ༭��ҳ���ϵ��������ɽڵ�
                NodeInfo node = new NodeInfo(@event.mousePosition);
                nodeList.Add(node);
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ���ƽڵ�
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
        /// �ڵ����
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