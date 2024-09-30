using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// �༭��
    /// </summary>
#if UNITY_EDITOR
    public class CustomLandMapEditor : EditorWindow
    {
        //���³ǻ���
        private static CustomLandMapCanvas _canvas;

        #region �򿪴���
        [MenuItem("���³Ƿ���༭��", menuItem = "�Զ���༭��/�����ͼ�༭��")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<CustomLandMapEditor>("�༭��");
        }

        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //ͨ��˫�������ʵ��id������ת��Ϊ����
            CustomLandMapCanvas so = EditorUtility.InstanceIDToObject(instanceID) as CustomLandMapCanvas;
            if (so == null) return false;

            OpenWindow();
            _canvas = so;

            //��ʼ����ʽ��Ⱦ
            _canvas.Initialize();

            return true;
        }

        #endregion

        private Vector3 startPosition = new Vector3(10, 0, 0);

        private void OnGUI()
        {
            DrawGrid();

            ProcessEvent();

            DrawNodes();
        }

        #region ����
        private void ProcessEvent()
        {
            var @event = Event.current;
            OnMouseDonwEventHandler(@event);
            OnRightMouseDonwEventHandler(@event);
        }

        /// <summary>
        /// ��갴���¼�����
        /// </summary>
        /// <param name="event"></param>
        private void OnMouseDonwEventHandler(Event @event)
        {
            if (@event.type == EventType.MouseDown && @event.button == 0)
            {
                CreateNewMapNode(@event.mousePosition);
            }
        }

        /// <summary>
        /// ��갴���¼�����
        /// </summary>
        /// <param name="event"></param>
        private void OnRightMouseDonwEventHandler(Event @event)
        {
            if (@event.type == EventType.MouseDown && @event.button == 1)
            {
                DeleteMapNode(@event.mousePosition);
            }
        }
        #endregion

        #region �¼�����
        /// <summary>
        /// ��Ԫ��ѡ��
        /// </summary>
        private void CreateNewMapNode(Vector3 mousePosition)
        {
            if (_canvas.NodeList == null) return;

            var gridPos = GetGridPosition(mousePosition, startPosition);
            var item = _canvas.NodeList.FirstOrDefault(p => p.originGridPosition.x == gridPos.x && p.originGridPosition.y == gridPos.y);
            if (item != null) return;

            var node = new CustomLandMapNode(gridPos, ConvertToCommonGridPosition(gridPos));
            node.Initilize();
            _canvas.NodeList.Add(node);

            //���ص������ڵ���
            AssetDatabase.AddObjectToAsset(node, _canvas);
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        /// <param name="mousePosition"></param>
        private void DeleteMapNode(Vector3 mousePosition)
        {
            var gridPos = GetGridPosition(mousePosition, startPosition);
            var item = _canvas.NodeList.FirstOrDefault(p => p.originGridPosition.x == gridPos.x && p.originGridPosition.y == gridPos.y);
            if (item != null)
            {
                _canvas.NodeList.Remove(item);

                //���ٽڵ�
                DestroyImmediate(item, true);
                //��ɾ����Ľڵ���µ�ǰ�Ľڵ�
                AssetDatabase.SaveAssets();

                GUI.changed = true;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �������񱳾�
        /// </summary>
        private void DrawGrid()
        {
            Handles.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1);

            for (int i = 0; i <= CustomLandMapCanvas.row; i++)
            {
                Handles.DrawLine(startPosition + new Vector3(0, i * CustomLandMapCanvas.gridSize), startPosition + new Vector3(CustomLandMapCanvas.col * CustomLandMapCanvas.gridSize, i * CustomLandMapCanvas.gridSize));
            }

            for (int i = 0; i <= CustomLandMapCanvas.col; i++)
            {
                Handles.DrawLine(startPosition + new Vector3(i * CustomLandMapCanvas.gridSize, 0), startPosition + new Vector3(i * CustomLandMapCanvas.gridSize, CustomLandMapCanvas.row * CustomLandMapCanvas.gridSize));
            }
        }

        /// <summary>
        /// �������нڵ�
        /// </summary>
        private void DrawNodes()
        {
            var nodeList = _canvas.NodeList;
            if (nodeList == null) return;

            foreach (var node in nodeList)
            {
                node.Draw(startPosition, CustomLandMapCanvas.gridSize);
            }
        }
        #endregion

        #region ���߷���
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private Vector2Int GetGridPosition(Vector3 mousePosition, Vector3 startPosition)
        {
            var actualGridPosition = mousePosition - startPosition;

            int rowIndex = (int)(actualGridPosition.x / CustomLandMapCanvas.gridSize);
            int colIndex = (int)(actualGridPosition.y / CustomLandMapCanvas.gridSize);

            return new Vector2Int(rowIndex, colIndex);
        }

        /// <summary>
        /// ת��Ϊ��׼����ϵ�����½�Ϊԭ��
        /// </summary>
        /// <returns></returns>
        private Vector2Int ConvertToCommonGridPosition(Vector2Int pos)
        {
            return new Vector2Int(pos.x, Mathf.Abs(CustomLandMapCanvas.row - 1 - pos.y));
        }

        #endregion
    }
#endif
}