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
    /// 设置后会挂载到画布下
    /// </summary>
    [CreateAssetMenu(fileName = "RoomNode_", menuName = "带UI演示/地下城/房间节点")]
    public class RoomNodeSO : ScriptableObject
    {
        public const float NodeWidth = 150;
        public const float NodeHeight = 75;
        public const int NodePadding = 20;
        public const int NodeBorder = 20;

        #region 属性
        //节点ID
        [HideInInspector] public string id;

        //父节点ID
        [HideInInspector] public string parentNodeId;

        //子节点ID集合
        [HideInInspector] public List<string> childNodeIdList = new List<string>();

        //节点的尺寸位置
        [HideInInspector] public Rect rect;

        //选中的房间类型
        [HideInInspector] public RoomNodeTypeSO selectedRoomNodeType;

        //节点是否被选中
        [HideInInspector] public bool isSelected;

        //节点是否正在被拖动
        [HideInInspector] public bool isDragging;

        //节点是否正在连线
        [HideInInspector] public bool isConnecting;

        //房间类型列表
        private List<RoomNodeTypeSO> roomTypeList;

        //弹窗列表[房间类型显示文本]
        private string[] popupTextList;

        //节点默认与选中样式
        private GUIStyle nodeStyle;
        private GUIStyle selectedNodeStyle;

        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(Vector2 position)
        {
            id = Guid.NewGuid().ToString();
            this.name = "RoomNode";

            //让节点其居中
            rect = new Rect(position.x - NodeWidth / 2, position.y - NodeHeight / 2, NodeWidth, NodeHeight);

            roomTypeList = GameResources.Instance.roomNodeTypeList.list;
            popupTextList = roomTypeList.Select(x => x.roomName).ToArray();

            //默认的房间类型
            selectedRoomNodeType = roomTypeList.FirstOrDefault(p => p.isNone);

            InitializeNodeStyle();
        }

        /// <summary>
        /// 初始化节点
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

        #region 事件触发
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
            isDragging = true;
            Debug.Log("开始拖拽");
        }

        /// <summary>
        /// 右键按下
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseDownEvent(Event @event)
        {
            isConnecting = true;
        }


        /// <summary>
        /// 左键抬起
        /// </summary>
        private void ProcessLeftMouseUpEvent(Event @event)
        {
            isDragging = false;
            Debug.Log("结束拖拽");
        }

        /// <summary>
        /// 右键抬起
        /// </summary>
        /// <param name="event"></param>
        private void ProcessRightMouseUpEvent(Event @event)
        {
            isConnecting = false;
        }

        /// <summary>
        /// 左键拖拽
        /// </summary>
        private void ProcessLeftMouseDragEvent(Event @event)
        {
            Debug.Log("拖拽中");
            if (isDragging)
            {
                this.rect.position += @event.delta;
            }
        }

        /// <summary>
        /// 右键拖拽
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
        /// 绘制节点
        /// </summary>
        public void Draw()
        {
            var style = GetNodeStyle();
            GUILayout.BeginArea(rect, style);

            //显示下拉列表，让用户选择房间类型
            //todo：此处不做限制
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
        /// 获取节点样式
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