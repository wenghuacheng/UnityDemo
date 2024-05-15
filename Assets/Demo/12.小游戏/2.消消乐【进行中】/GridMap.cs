using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    /// <summary>
    /// 地图网格
    /// </summary>
    public class GridMap : MonoBehaviour
    {
        public event Action OnCellEliminate;//元素被消除

        [SerializeField] private GameObject gridPerfab;
        [SerializeField] private SpriteRenderer temp;

        [Header("网格字段")]
        private GridCellData[,] grid;
        private GridCellUI[,] gridUI;
        private int row = 5;
        private int col = 5;
        private int gridSize = 3;
        private Vector2 startPosition = Vector2.zero;

        [Header("绘制网格")]
        private List<LineRenderer> lines = new List<LineRenderer>();

        [Header("网格交互")]
        private GridCellData currentCell;
        private Vector2 clickPosition;

        private Camera _camera;

        #region 初始化
        private void Awake()
        {
            _camera = Camera.main;

            InitializeGrid();
            InitializeGridUI();
            DrawGrid();
        }

        /// <summary>
        /// 初始化网格
        /// </summary>
        private void InitializeGrid()
        {
            grid = new GridCellData[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GridCellData newItem = new GridCellData(i, j);
                    newItem.center = startPosition + new Vector2(i * gridSize + gridSize / 2, j * gridSize + gridSize / 2);
                    newItem.rect = new Rect(i * gridSize, j * gridSize, gridSize, gridSize);
                    grid[i, j] = newItem;
                }
            }
        }

        /// <summary>
        /// 初始化网络UI显示
        /// </summary>
        private void InitializeGridUI()
        {
            gridUI = new GridCellUI[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var cell = grid[i, j];
                    var obj = Instantiate(gridPerfab, cell.center, Quaternion.identity, this.transform);
                    var cellUI = obj.GetComponent<GridCellUI>();
                    gridUI[i, j] = cellUI;
                    cellUI.SetIcon(temp.sprite);
                }
            }
        }

        #endregion

        void Update()
        {
            HandleCellEvent();
        }

        #region 绘制
        /// <summary>
        /// 绘制网格
        /// </summary>
        private void DrawGrid()
        {
            for (int i = 0; i <= row; i++)
            {
                LineRenderer line = CreateLineRenderer();
                var start = startPosition + new Vector2(0, i * gridSize);
                var end = startPosition + new Vector2(col * gridSize, i * gridSize);
                line.SetPositions(new Vector3[] { start, end });
                lines.Add(line);
            }

            for (int i = 0; i <= col; i++)
            {
                LineRenderer line = CreateLineRenderer();
                var start = startPosition + new Vector2(i * gridSize, 0);
                var end = startPosition + new Vector2(i * gridSize, col * gridSize);
                line.SetPositions(new Vector3[] { start, end });
                lines.Add(line);
            }
        }

        private LineRenderer CreateLineRenderer()
        {
            GameObject go = new GameObject("LineRenderer");
            go.transform.SetParent(transform);
            var line = go.AddComponent<LineRenderer>();
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
            line.startColor = Color.white;
            line.endColor = Color.white;
            return line;
        }
        #endregion

        #region Propeties
        public GridCellData[,] Grid { get { return grid; } }

        public int Row { get { return row; } }

        public int Col { get { return col; } }
        #endregion

        /// <summary>
        /// 单元格交互
        /// </summary>
        private void HandleCellEvent()
        {
            #region 鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        var cell = grid[i, j];
                        if (cell.Contains(clickPosition))
                        {
                            currentCell = grid[i, j];
                            break;
                        }

                    }
                }
                return;
            }
            #endregion

            #region 鼠标拖动
            if (Input.GetMouseButton(0))
            {
                var p = _camera.ScreenToWorldPoint(Input.mousePosition);
                var cells = FindMatchCell(clickPosition, p);
                foreach (var cell in cells)
                {
                    if (cell.Contains(p))
                    {
                        //匹配到位置
                        ExchangeCell(currentCell, cell);

                        currentCell = null;
                        return;
                    }
                }
            }
            #endregion

            #region 鼠标抬起
            if (Input.GetMouseButtonUp(0))
            {
                currentCell = null;
                clickPosition = Vector2.zero;
                return;
            }
            #endregion
        }

        /// <summary>
        /// 基于鼠标移动方向找到可匹配的单元格
        /// </summary>
        private List<GridCellData> FindMatchCell(Vector2 clickPosition, Vector2 curPositon)
        {
            List<GridCellData> result = new List<GridCellData>();
            if (currentCell == null) return result;

            var intervalX = clickPosition.x - curPositon.x;
            var intervalY = clickPosition.y - curPositon.y;

            #region 基于当前鼠标位置判断可能进入的单元格

            //判定X轴方法可能匹配的单元格
            if (intervalX > 0)
            {
                //当前鼠标位置在点击位置的左侧，应该找到左侧的单元格
                int cellX = currentCell.x - 1;
                if (cellX >= 0)
                    result.Add(grid[cellX, currentCell.y]);
            }
            else if (intervalX < 0)
            {
                //当前鼠标位置在点击位置的右侧，应该找到右侧的单元格
                int cellX = currentCell.x + 1;
                if (cellX < col)
                    result.Add(grid[cellX, currentCell.y]);
            }

            //判定Y轴方法可能匹配的单元格
            if (intervalY > 0)
            {
                //当前鼠标位置在点击位置的下方，应该找到下方的单元格
                int cellY = currentCell.y - 1;
                if (cellY >= 0)
                    result.Add(grid[currentCell.x, cellY]);
            }
            else if (intervalY < 0)
            {
                //当前鼠标位置在点击位置的上方，应该找到上方的单元格
                int cellY = currentCell.y + 1;
                if (cellY < row)
                    result.Add(grid[currentCell.x, cellY]);
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 交换单元格
        /// </summary>
        private void ExchangeCell(GridCellData cell1, GridCellData cell2)
        {
            Debug.Log(cell1.center + "-" + cell2.center);
        }
    }
}
