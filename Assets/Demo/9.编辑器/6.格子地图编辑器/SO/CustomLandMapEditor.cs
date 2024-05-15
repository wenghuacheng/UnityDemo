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
    /// 编辑器
    /// </summary>
#if UNITY_EDITOR
    public class CustomLandMapEditor : EditorWindow
    {
        //地下城画布
        private static CustomLandMapCanvas _canvas;

        #region 打开窗体
        [MenuItem("地下城房间编辑器", menuItem = "自定义编辑器/网格地图编辑器")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<CustomLandMapEditor>("编辑器");
        }

        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //通过双击对象的实例id，将其转化为对象
            CustomLandMapCanvas so = EditorUtility.InstanceIDToObject(instanceID) as CustomLandMapCanvas;
            if (so == null) return false;

            OpenWindow();
            _canvas = so;

            //初始化样式渲染
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

        #region 交互
        private void ProcessEvent()
        {
            var @event = Event.current;
            OnMouseDonwEventHandler(@event);
            OnRightMouseDonwEventHandler(@event);
        }

        /// <summary>
        /// 鼠标按下事件处理
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
        /// 鼠标按下事件处理
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

        #region 事件处理
        /// <summary>
        /// 单元格选择
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

            //挂载到画布节点下
            AssetDatabase.AddObjectToAsset(node, _canvas);
            AssetDatabase.SaveAssets();

            GUI.changed = true;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="mousePosition"></param>
        private void DeleteMapNode(Vector3 mousePosition)
        {
            var gridPos = GetGridPosition(mousePosition, startPosition);
            var item = _canvas.NodeList.FirstOrDefault(p => p.originGridPosition.x == gridPos.x && p.originGridPosition.y == gridPos.y);
            if (item != null)
            {
                _canvas.NodeList.Remove(item);

                //销毁节点
                DestroyImmediate(item, true);
                //将删除后的节点更新当前的节点
                AssetDatabase.SaveAssets();

                GUI.changed = true;
            }
        }
        #endregion

        #region 绘制网格
        /// <summary>
        /// 绘制网格背景
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
        /// 绘制所有节点
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

        #region 工具方法
        /// <summary>
        /// 获取网格索引
        /// </summary>
        private Vector2Int GetGridPosition(Vector3 mousePosition, Vector3 startPosition)
        {
            var actualGridPosition = mousePosition - startPosition;

            int rowIndex = (int)(actualGridPosition.x / CustomLandMapCanvas.gridSize);
            int colIndex = (int)(actualGridPosition.y / CustomLandMapCanvas.gridSize);

            return new Vector2Int(rowIndex, colIndex);
        }

        /// <summary>
        /// 转换为标准坐标系，左下角为原点
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