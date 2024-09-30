using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// ��������ڵ�����
    /// </summary>
#if UNITY_EDITOR
    public class CustomLandMapNode : ScriptableObject
    {
        private GUIStyle nodeStyle;
        private const int NodePadding = 0;
        private const int NodeBorder = 0;

        #region ��ʼ��
        public CustomLandMapNode(Vector2Int originGridPosition, Vector2Int commonGridPosition)
        {
            this.originGridPosition = originGridPosition;
            this.commonGridPosition = commonGridPosition;
            Debug.Log(originGridPosition + "-" + this.commonGridPosition);
        }

        /// <summary>
        /// ��ʼ��
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

        //�ڵ�����
        public CustomLandMapNodeType type = CustomLandMapNodeType.None;

        //�ڱ༭���е���������(�����Ͻ�δԭ�㣬���º�����Ϊ������)
        public Vector2Int originGridPosition;

        //���½�Ϊԭ�������ϵ
        public Vector2Int commonGridPosition;

        /// <summary>
        /// ���ƽڵ�
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