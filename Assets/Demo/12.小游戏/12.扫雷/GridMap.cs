using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MineSweep
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private int row = 8;
        [SerializeField] private int col = 8;
        [SerializeField] private Cell cellPrefab;

        private Camera _camera;

        //网格尺寸
        private int gridSize = 10;
        //地雷数量
        private int maxMineCount = 20;
        //逻辑网格(0:正常，1：地雷)
        private int[,] map;
        //UI网格
        private Cell[,] cells;

        //地图初始化位置
        private Vector3 mapStartPosition = Vector3.zero;

        #region 初始化
        private void Start()
        {
            _camera = Camera.main;

            InitializeMap();
            InitializeVisual();
        }

        /// <summary>
        /// 初始化逻辑地图
        /// </summary>
        private void InitializeMap()
        {
            map = new int[row, col];
            //随机地雷
            int mineCount = 0;
            while (mineCount < maxMineCount)
            {
                var rowIndex = Random.Range(0, row);
                var colIndex = Random.Range(0, col);

                if (map[rowIndex, colIndex] == 1)
                    continue;

                map[rowIndex, colIndex] = 1;
                mineCount++;
            }
        }

        /// <summary>
        /// 初始化显示地图
        /// </summary>
        private void InitializeVisual()
        {
            cells = new Cell[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Vector2 pos = GetGridPosition(i, j);
                    var cell = Instantiate(cellPrefab, pos, Quaternion.identity, this.transform);
                    //设置当前单元格为地雷
                    if (map[i, j] == 1)
                        cell.isMineCell = true;

                    cell.SetStatus(0);
                    cells[i, j] = cell;
                }
            }
        }
        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                CellClickHandler();
        }

        /// <summary>
        /// 单元格点击
        /// </summary>
        private void CellClickHandler()
        {
            var vec = GetClickGridIndex();
            if (!IsVaildCell(vec)) return;

            if (cells[vec.x, vec.y].isMineCell)
            {
                Debug.Log("踩到地雷了");
                var curUICell = cells[vec.x, vec.y];
                curUICell.SetStatus(1);
                return;
            }
            else if (cells[vec.x, vec.y].Status == 1)
            {
                return;
            }

            //开始搜索当个格子与四周的格子
            SearchAroundCell(vec);
        }

        /// <summary>
        /// 8个方向的单元格检查
        /// </summary>
        /// <param name="positon"></param>
        private void SearchAroundCell(Vector2Int position)
        {
            if (!IsVaildCell(position))
                return;

            var curUICell = cells[position.x, position.y];

            //当前标记为已搜索
            curUICell.SetStatus(1);

            int mineCount = 0;
            List<Vector2Int> aroundPostion = new List<Vector2Int>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    var p = new Vector2Int(position.x + i, position.y + j);
                    if (!IsVaildCell(p))
                        continue;

                    var cell = cells[p.x, p.y];

                    //地雷节点
                    if (cell.isMineCell)
                    {
                        mineCount++;
                    }

                    if (cell.Status == 0)
                    {
                        //未搜索的格子
                        aroundPostion.Add(p);
                    }
                }
            }

            //刷新单元格UI
            curUICell.SetMineCount(mineCount);

            //只有周围没有地雷才扩展
            if (mineCount <= 0)
            {
                foreach (var p in aroundPostion)
                {
                    SearchAroundCell(p);
                }
            }
        }

        #region 网格方法

        /// <summary>
        /// 获取网格位置
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGridPosition(int rowIndex, int colIndex)
        {
            return mapStartPosition + new Vector3(rowIndex * gridSize, colIndex * gridSize);
        }

        /// <summary>
        /// 获取点击的网格索引
        /// </summary>
        /// <returns></returns>
        private Vector2Int GetClickGridIndex()
        {
            var clickPos = _camera.ScreenToWorldPoint(Input.mousePosition) - mapStartPosition;

            //由于中心点问题，需要偏移半个单元格
            int xIndex = (int)((clickPos.x + gridSize / 2) / gridSize);
            int yIndex = (int)((clickPos.y + gridSize / 2) / gridSize);

            if (xIndex > row - 1 || yIndex > col - 1 || xIndex < 0 || yIndex < 0)
                return new Vector2Int(-1, -1);
            else
                return new Vector2Int(xIndex, yIndex);
        }

        /// <summary>
        /// 是否是合法的单元格
        /// </summary>
        /// <returns></returns>
        private bool IsVaildCell(Vector2 position)
        {
            if (position.x > row - 1 || position.y > col - 1 || position.x < 0 || position.y < 0)
                return false;
            else
                return true;
        }
        #endregion

    }
}