using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// 单个网格节点数据
    /// </summary>
#if UNITY_EDITOR
    public class CustomLandMapNode : ScriptableObject
    {
        private GUIStyle nodeStyle;
        private const int NodePadding = 0;
        private const int NodeBorder = 0;

        #region 初始化
        public CustomLandMapNode(Vector2Int originGridPosition, Vector2Int commonGridPosition)
        {
            this.originGridPosition = originGridPosition;
            this.commonGridPosition = commonGridPosition;
            Debug.Log(originGridPosition + "-" + this.commonGridPosition);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initilize()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
            nodeStyle.border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder);
        }
        #endregion

        //节点类型
        public CustomLandMapNodeType type = CustomLandMapNodeType.None;

        //在编辑器中的网格索引(以左上角未原点，向下和向右为正方向)
        public Vector2Int originGridPosition;

        //左下角为原点的坐标系
        public Vector2Int commonGridPosition;

        /// <summary>
        /// 绘制节点
        /// </summary>
        public void Draw(Vector3 startPositon, int gridSize)
        {
            var pos = startPositon + new Vector3(originGridPosition.x * gridSize, originGridPosition.y * gridSize);
            var rect = new Rect(pos, new Vector2(gridSize, gridSize));

            GUILayout.BeginArea(rect, nodeStyle);

            type = (CustomLandMapNodeType)EditorGUILayout.EnumPopup(type);

            GUILayout.EndArea();
        }
    }
#endif
}