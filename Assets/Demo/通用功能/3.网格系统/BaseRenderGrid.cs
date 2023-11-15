using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// 基础渲染
    /// </summary>
    public abstract class BaseRenderGrid : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] protected int row;
        [SerializeField] protected int col;
        [SerializeField] protected Material lineMaterial;
        [SerializeField] protected Camera _camera;
        #endregion

        protected float gridWidth = 10f;
        protected float lineWidth = 0.2f;
        protected Vector3 gridOffest = Vector3.zero;//网格起始点的偏移量
        protected List<LineRenderer> lineRenderList = new List<LineRenderer>();

        protected virtual void Awake()
        {

        }


        protected virtual void Start()
        {
            DrawGrid();
        }

        protected virtual void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //触发单元格点击事件处理
                var cellPosition = GetMouseClickCellPosition(gridOffest);
                if (!IsValidCellPosition(cellPosition)) return;
                CellClickHandler(cellPosition);
            }
        }

        /// <summary>
        /// 单元格点击
        /// </summary>
        /// <param name="position"></param>
        protected abstract void CellClickHandler(Vector2Int position);

        #region 元素绘制

        /// <summary>
        /// 绘制网格
        /// </summary>
        protected void DrawGrid()
        {
            //绘制行网格线
            for (int i = 0; i <= row; i++)
            {
                Vector2 startPosition = new Vector2(0, i * gridWidth);
                Vector2 endPosition = new Vector2(col * gridWidth, i * gridWidth);
                CreateRenderLine(startPosition, endPosition, lineMaterial, lineWidth);
            }

            //绘制列网格线
            for (int i = 0; i <= col; i++)
            {
                Vector2 startPosition = new Vector2(i * gridWidth, 0);
                Vector2 endPosition = new Vector2(i * gridWidth, row * gridWidth);
                CreateRenderLine(startPosition, endPosition, lineMaterial, lineWidth);
            }
        }

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="lineMaterial"></param>
        /// <param name="lineWidth"></param>
        protected void CreateRenderLine(Vector2 startPosition, Vector2 endPosition, Material lineMaterial, float lineWidth = 0.2f)
        {
            GameObject go = new GameObject("LineRenderer");
            go.transform.SetParent(transform);
            var renderer = go.AddComponent<LineRenderer>();
            renderer.startWidth = lineWidth;
            renderer.endWidth = lineWidth;
            renderer.SetPositions(new Vector3[] { startPosition, endPosition });
            renderer.material = lineMaterial;
            lineRenderList.Add(renderer);
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 是否有效的单元格位置
        /// </summary>
        /// <returns></returns>
        protected bool IsValidCellPosition(Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < row && position.y < col;
        }

        /// <summary>
        /// 获取鼠标点击的单元格
        /// </summary>
        protected Vector2Int GetMouseClickCellPosition(Vector3 offest)
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - offest;//需要减去原点偏移
            int x = (int)(worldPosition.x / gridWidth);
            int y = (int)(worldPosition.y / gridWidth);
            var position = new Vector2Int(x, y);
            return position;
        }


        #endregion
    }
}