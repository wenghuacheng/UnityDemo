using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace Demo.DungeonEditor
{
    /// <summary>
    /// ���³Ǳ༭��
    /// </summary>
#if UNITY_EDITOR
    public class DungeonRoomEditor : EditorWindow
    {
        //���³ǻ���
        private static RoomGraphCanvas _canvas;

        //��ǰ��ק�ķ���ڵ�
        private RoomNodeSO currentDraggingNode;

        //��ǰ���ߵķ���ڵ�
        private RoomNodeSO currentConnectNode;

        //Ctrl���Ƿ񱻰���
        private bool isCtrlPress;

        #region �򿪱༭��
        [MenuItem("���³Ƿ���༭��", menuItem = "�Զ���༭����ʾ/���³Ƿ���༭��")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<DungeonRoomEditor>("���³Ƿ���༭��");
        }

        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //ͨ��˫�������ʵ��id������ת��Ϊ����
            RoomGraphCanvas so = EditorUtility.InstanceIDToObject(instanceID) as RoomGraphCanvas;
            if (so == null) return false;

            OpenWindow();
            _canvas = so;

            return true;
        }
        #endregion

        private void OnGUI()
        {
            ProcessEvent();

            DrawConnectingLine();

            DrawConnectedLine();

            DrawNodes();
        }

        #region �¼�����
        private void ProcessEvent()
        {
            Event @event = Event.current;

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

            isCtrlPress = @event.control;
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
            SetNodeSelected(@event.mousePosition);

            if (currentDraggingNode == null)
                currentDraggingNode = MatchNode(@event.mousePosition);

            if (currentDraggingNode != null)
                currentDraggingNode.ProcessEvent(@event);
        }

        /// <summary>
        /// �Ҽ�����
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDownEvent(Event @event)
        {
            currentConnectNode = MatchNode(@event.mousePosition);

            if (currentConnectNode != null)
            {
                //�ڵ��Ҽ�
                _canvas.lineStartPosition = @event.mousePosition;
                _canvas.lineEndPosition = @event.mousePosition;
            }
            else
            {
                //�����Ҽ�
                ShowContextMenu(@event.mousePosition);
            }
        }


        /// <summary>
        /// ���̧��
        /// </summary>
        private void ProcessLeftMouseUpEvent(Event @event)
        {
            if (currentDraggingNode != null)
            {
                currentDraggingNode.ProcessEvent(@event);
            }

            currentDraggingNode = null;
            currentConnectNode = null;
        }

        /// <summary>
        /// �Ҽ�̧��
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseUpEvent(Event @event)
        {
            if (currentConnectNode != null)
            {
                //���ӽڵ�
                EndConnected(currentConnectNode, @event.mousePosition);

                currentConnectNode.ProcessEvent(@event);
                currentConnectNode.isConnecting = false;
            }

            currentDraggingNode = null;
            currentConnectNode = null;
        }

        /// <summary>
        /// �����ק
        /// </summary>
        private void ProcessLeftMouseDragEvent(Event @event)
        {
            if (currentDraggingNode != null)
            {
                currentDraggingNode.ProcessEvent(@event);
            }
        }

        /// <summary>
        /// �Ҽ���ק
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDragEvent(Event @event)
        {
            if (currentConnectNode != null)
                _canvas.lineEndPosition = @event.mousePosition;
        }
        #endregion

        #endregion

        #region �¼����� 

        /// <summary>
        /// ��ʾ�Ҽ��˵�
        /// </summary>
        private void ShowContextMenu(Vector2 mousePosition)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("�����ڵ�"), false, CreateRoomNode, mousePosition);
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("ɾ������"), false, RemoveRoomNodeConnection);
            menu.AddItem(new GUIContent("ɾ���ڵ�"), false, RemoveRoomNode);
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("���ѡ��״̬"), false, ClearSelectedNodeState);

            menu.ShowAsContext();
        }

        /// <summary>
        /// ��������ڵ�
        /// </summary>
        private void CreateRoomNode(object positionObj)
        {
            Vector2 position = (Vector2)positionObj;

            RoomNodeSO roomNode = new RoomNodeSO();

            roomNode.Initialize(position);

            _canvas.AddNode(roomNode);

            //���ص������ڵ���
            AssetDatabase.AddObjectToAsset(roomNode, _canvas);
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        private void RemoveRoomNodeConnection()
        {
            var selectedNodeList = _canvas.nodeList.Where(p => p.isSelected).ToList();

            foreach (var selectedNode in selectedNodeList)
            {
                RemoveRoomNodeConnection(selectedNode);
            }

            GUI.changed = true;
        }

        /// <summary>
        /// ɾ��������������
        /// </summary>
        /// <param name="node"></param>
        private void RemoveRoomNodeConnection(RoomNodeSO node)
        {
            List<string> removeList = new List<string>();

            foreach (var childId in node.childNodeIdList)
            {
                var childNode = _canvas.GetNode(childId);
                if (childNode == null) continue;

                childNode.parentNodeId = string.Empty;
                removeList.Add(childNode.id);
            }

            foreach (var childId in removeList)
            {
                node.childNodeIdList.Remove(childId);
            }

            GUI.changed = true;
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        private void RemoveRoomNode()
        {
            var selectedNodeList = _canvas.nodeList.Where(p => p.isSelected).ToList();

            foreach (var selectedNode in selectedNodeList)
            {
                //ɾ������ڵ����ӽڵ������
                RemoveRoomNodeConnection(selectedNode);
                //ɾ���ڵ�
                _canvas.RemoveNode(selectedNode);

                //���ٽڵ�
                DestroyImmediate(selectedNode, true);
            }

            //��ɾ����Ľڵ���µ�ǰ�Ľڵ�
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// ѡ�нڵ�
        /// </summary>
        private void SetNodeSelected(Vector2 position)
        {
            var matchNode = MatchNode(position);
            if (matchNode != null)
            {
                if (!isCtrlPress)
                    ClearSelectedNodeState();

                matchNode.isSelected = true;
            }

            GUI.changed = true;
        }

        /// <summary>
        /// ����ڵ�ѡ��״̬
        /// </summary>
        private void ClearSelectedNodeState()
        {
            foreach (var node in _canvas.nodeList)
            {
                node.isSelected = false;
            }
            GUI.changed = true;
        }

        /// <summary>
        /// ƥ��ڵ�
        /// </summary>
        /// <param name="mousePosition"></param>
        private RoomNodeSO MatchNode(Vector2 mousePosition)
        {
            for (int i = _canvas.nodeList.Count - 1; i >= 0; i--)
            {
                var node = _canvas.nodeList[i];

                if (node.rect.Contains(mousePosition))
                    return node;
            }
            return null;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void EndConnected(RoomNodeSO startNode, Vector2 mousePosition)
        {
            if (startNode == null) return;

            var childNode = MatchNode(mousePosition);
            if (childNode == null) return;

            //���ø��ӽڵ�Ĺ�����ϵ
            childNode.parentNodeId = startNode.id;
            if (!startNode.childNodeIdList.Contains(childNode.id))
                startNode.childNodeIdList.Add(childNode.id);
        }
        #endregion

        #region �����߼�

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        public void DrawNodes()
        {
            foreach (var node in _canvas.nodeList)
            {
                node.Draw();
            }
        }

        /// <summary>
        /// ���������е���
        /// </summary>
        private void DrawConnectingLine()
        {
            if (currentConnectNode != null)
            {
                Vector2 startPosition = _canvas.lineStartPosition;
                Vector2 endPosition = _canvas.lineEndPosition;
                Handles.DrawBezier(startPosition, endPosition, startPosition, endPosition, Color.white, null, 5f);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        private void DrawConnectedLine()
        {
            foreach (var node in _canvas.nodeList)
            {
                foreach (var childNodeId in node.childNodeIdList)
                {
                    var childNode = _canvas.GetNode(childNodeId);
                    if (childNode == null) continue;

                    float lineWidth = 5f;

                    //����
                    Vector2 startPosition = node.rect.center;
                    Vector2 endPosition = childNode.rect.center;
                    Handles.DrawBezier(startPosition, endPosition, startPosition, endPosition, Color.white, null, lineWidth);

                    //����ͷ
                    var direction = (endPosition - startPosition).normalized;
                    var centerPosition = (startPosition + endPosition) / 2;

                    Vector2 arrowTail01 = centerPosition + new Vector2(direction.y, -direction.x) * 5;
                    Vector2 arrowTail02 = centerPosition - new Vector2(direction.y, -direction.x) * 5;
                    Vector2 header = centerPosition + direction * 5;

                    Handles.DrawBezier(header, arrowTail01, header, arrowTail01, Color.white, null, lineWidth);
                    Handles.DrawBezier(header, arrowTail02, header, arrowTail02, Color.white, null, lineWidth);
                }
            }
        }
        #endregion
    }
#endif
}