using Demo.CustomEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Demo.DungeonEditor
{
    /// <summary>
    /// 地下城编辑器
    /// </summary>
    public class DungeonRoomEditor : EditorWindow
    {
        //地下城画布
        private static RoomGraphCanvas _canvas;

        //当前拖拽的房间节点
        private RoomNodeSO currentDraggingNode;

        //当前连线的房间节点
        private RoomNodeSO currentConnectNode;

        //Ctrl键是否被按下
        private bool isCtrlPress;

        #region 打开编辑器
        [MenuItem("地下城房间编辑器", menuItem = "自定义编辑器演示/地下城房间编辑器")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<DungeonRoomEditor>("地下城房间编辑器");
        }

        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //通过双击对象的实例id，将其转化为对象
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

        #region 事件触发
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

        #region 鼠标按下&拖拽&鼠标抬起
        /// <summary>
        /// 鼠标按下
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
        /// 鼠标抬起
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
        /// 鼠标拖拽
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

        #region 鼠标左右键事件处理
        /// <summary>
        /// 左键按下
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
        /// 右键按下
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDownEvent(Event @event)
        {
            currentConnectNode = MatchNode(@event.mousePosition);

            if (currentConnectNode != null)
            {
                //节点右键
                _canvas.lineStartPosition = @event.mousePosition;
                _canvas.lineEndPosition = @event.mousePosition;
            }
            else
            {
                //画布右键
                ShowContextMenu(@event.mousePosition);
            }
        }


        /// <summary>
        /// 左键抬起
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
        /// 右键抬起
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseUpEvent(Event @event)
        {
            if (currentConnectNode != null)
            {
                //连接节点
                EndConnected(currentConnectNode, @event.mousePosition);

                currentConnectNode.ProcessEvent(@event);
                currentConnectNode.isConnecting = false;
            }

            currentDraggingNode = null;
            currentConnectNode = null;
        }

        /// <summary>
        /// 左键拖拽
        /// </summary>
        private void ProcessLeftMouseDragEvent(Event @event)
        {
            if (currentDraggingNode != null)
            {
                currentDraggingNode.ProcessEvent(@event);
            }
        }

        /// <summary>
        /// 右键拖拽
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDragEvent(Event @event)
        {
            if (currentConnectNode != null)
                _canvas.lineEndPosition = @event.mousePosition;
        }
        #endregion

        #endregion

        #region 事件处理 

        /// <summary>
        /// 显示右键菜单
        /// </summary>
        private void ShowContextMenu(Vector2 mousePosition)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("创建节点"), false, CreateRoomNode, mousePosition);
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("删除连接"), false, RemoveRoomNodeConnection);
            menu.AddItem(new GUIContent("删除节点"), false, RemoveRoomNode);
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("清除选中状态"), false, ClearSelectedNodeState);

            menu.ShowAsContext();
        }

        /// <summary>
        /// 创建房间节点
        /// </summary>
        private void CreateRoomNode(object positionObj)
        {
            Vector2 position = (Vector2)positionObj;

            RoomNodeSO roomNode = new RoomNodeSO();

            roomNode.Initialize(position);

            _canvas.AddNode(roomNode);

            //挂载到画布节点下
            AssetDatabase.AddObjectToAsset(roomNode, _canvas);
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// 删除房间连接
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
        /// 删除单个房间连接
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
        /// 删除房间
        /// </summary>
        private void RemoveRoomNode()
        {
            var selectedNodeList = _canvas.nodeList.Where(p => p.isSelected).ToList();

            foreach (var selectedNode in selectedNodeList)
            {
                //删除这个节点与子节点的连接
                RemoveRoomNodeConnection(selectedNode);
                //删除节点
                _canvas.RemoveNode(selectedNode);

                //销毁节点
                DestroyImmediate(selectedNode, true);
            }

            //将删除后的节点更新当前的节点
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// 选中节点
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
        /// 清除节点选中状态
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
        /// 匹配节点
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
        /// 结束连线
        /// </summary>
        private void EndConnected(RoomNodeSO startNode, Vector2 mousePosition)
        {
            if (startNode == null) return;

            var childNode = MatchNode(mousePosition);
            if (childNode == null) return;

            //设置父子节点的关联关系
            childNode.parentNodeId = startNode.id;
            if (!startNode.childNodeIdList.Contains(childNode.id))
                startNode.childNodeIdList.Add(childNode.id);
        }
        #endregion

        #region 绘制逻辑

        /// <summary>
        /// 绘制节点
        /// </summary>
        public void DrawNodes()
        {
            foreach (var node in _canvas.nodeList)
            {
                node.Draw();
            }
        }

        /// <summary>
        /// 绘制连接中的线
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
        /// 绘制已连接线
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

                    //画线
                    Vector2 startPosition = node.rect.center;
                    Vector2 endPosition = childNode.rect.center;
                    Handles.DrawBezier(startPosition, endPosition, startPosition, endPosition, Color.white, null, lineWidth);

                    //画箭头
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
}