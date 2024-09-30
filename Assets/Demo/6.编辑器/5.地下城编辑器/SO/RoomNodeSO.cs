using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.DungeonEditor
{
#if UNITY_EDITOR
    /// <summary>
    /// ���ú����ص�������
    /// </summary>
    [CreateAssetMenu(fileName = "RoomNode_", menuName = "��UI��ʾ/���³�/����ڵ�")]
    public class RoomNodeSO : ScriptableObject
    {
        public const float NodeWidth = 150;
        public const float NodeHeight = 75;
        public const int NodePadding = 20;
        public const int NodeBorder = 20;

        #region ����
        //�ڵ�ID
        [HideInInspector] public string id;

        //���ڵ�ID
        [HideInInspector] public string parentNodeId;

        //�ӽڵ�ID����
        [HideInInspector] public List<string> childNodeIdList = new List<string>();

        //�ڵ�ĳߴ�λ��
        [HideInInspector] public Rect rect;

        //ѡ�еķ�������
        [HideInInspector] public RoomNodeTypeSO selectedRoomNodeType;

        //�ڵ��Ƿ�ѡ��
        [HideInInspector] public bool isSelected;

        //�ڵ��Ƿ����ڱ��϶�
        [HideInInspector] public bool isDragging;

        //�ڵ��Ƿ���������
        [HideInInspector] public bool isConnecting;

        //���������б�
        private List<RoomNodeTypeSO> roomTypeList;

        //�����б�[����������ʾ�ı�]
        private string[] popupTextList;

        //�ڵ�Ĭ����ѡ����ʽ
        private GUIStyle nodeStyle;
        private GUIStyle selectedNodeStyle;

        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Initialize(Vector2 position)
        {
            id = Guid.NewGuid().ToString();
            this.name = "RoomNode";

            //�ýڵ������
            rect = new Rect(position.x - NodeWidth / 2, position.y - NodeHeight / 2, NodeWidth, NodeHeight);

            roomTypeList = GameResources.Instance.roomNodeTypeList.list;
            popupTextList = roomTypeList.Select(x => x.roomName).ToArray();

            //Ĭ�ϵķ�������
            selectedRoomNodeType = roomTypeList.FirstOrDefault(p => p.isNone);

            InitializeNodeStyle();
        }

        /// <summary>
        /// ��ʼ���ڵ�
        /// </summary>
        private void InitializeNodeStyle()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
            nodeStyle.border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("node1 on") as Texture2D;
            selectedNodeStyle.normal.textColor = Color.white;
            selectedNodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
            selectedNodeStyle.border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder);
        }
        #endregion

        #region �¼�����
        public void ProcessEvent(Event @event)
        {
            if (@event.type == EventType.MouseDown)
            {
                ProcessMouseDownEvent(@event);
            }
            else if (@event.type == EventType.MouseUp)
            {
                ProcessMouseUpEvent(@event);
            }
            else if (@event.type == EventType.MouseDrag)
            {
                ProcessMouseDragEvent(@event);
            }
        }

        #region ��갴��&��ק&���̧��
        /// <summary>
        /// ��갴��
        /// </summary>
        private void ProcessMouseDownEvent(Event @event)
        {
            if (@event.button == 0)
            {
                ProcessLeftMouseDownEvent(@event);
            }
            else if (@event.button == 1)
            {
                ProcessRightMouseDownEvent(@event);
            }
        }

        /// <summary>
        /// ���̧��
        /// </summary>
        private void ProcessMouseUpEvent(Event @event)
        {
            if (@event.button == 0)
            {
                ProcessLeftMouseUpEvent(@event);
            }
            else if (@event.button == 1)
            {
                ProcessRightMouseUpEvent(@event);
            }
        }

        /// <summary>
        /// �����ק
        /// </summary>
        private void ProcessMouseDragEvent(Event @event)
        {
            if (@event.button == 0)
            {
                ProcessLeftMouseDragEvent(@event);
            }
            else if (@event.button == 1)
            {
                ProcessRightMouseDragEvent(@event);
            }
        }

        #endregion

        #region ������Ҽ��¼�����
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="event"></param>
        private void ProcessLeftMouseDownEvent(Event @event)
        {
            isDragging = true;
            Debug.Log("��ʼ��ק");
        }

        /// <summary>
        /// �Ҽ�����
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDownEvent(Event @event)
        {
            isConnecting = true;
        }


        /// <summary>
        /// ���̧��
        /// </summary>
        private void ProcessLeftMouseUpEvent(Event @event)
        {
            isDragging = false;
            Debug.Log("������ק");
        }

        /// <summary>
        /// �Ҽ�̧��
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseUpEvent(Event @event)
        {
            isConnecting = false;
        }

        /// <summary>
        /// �����ק
        /// </summary>
        private void ProcessLeftMouseDragEvent(Event @event)
        {
            Debug.Log("��ק��");
            if (isDragging)
            {
                this.rect.position += @event.delta;
            }
        }

        /// <summary>
        /// �Ҽ���ק
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDragEvent(Event @event)
        {
            if (isConnecting)
            {

            }
        }
        #endregion

        #endregion

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        public void Draw()
        {
            var style = GetNodeStyle();
            GUILayout.BeginArea(rect, style);

            //��ʾ�����б����û�ѡ�񷿼�����
            //todo���˴���������
            int currentSelectedIndex = roomTypeList.IndexOf(selectedRoomNodeType);
            int selection = EditorGUILayout.Popup("", currentSelectedIndex, popupTextList);
            selectedRoomNodeType = roomTypeList[selection];

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(this);
            }

            GUILayout.EndArea();
        }

        /// <summary>
        /// ��ȡ�ڵ���ʽ
        /// </summary>
        private GUIStyle GetNodeStyle()
        {
            if (isSelected)
                return selectedNodeStyle;
            else
                return nodeStyle;
        }
    }
#endif
}