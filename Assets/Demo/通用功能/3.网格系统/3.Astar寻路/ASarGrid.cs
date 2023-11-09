using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Common.Grids
{
    public class ASarGrid : MonoBehaviour
    {
        [SerializeField] private int row;
        [SerializeField] private int col;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private GameObject gridPrefab;

        [SerializeField] private Camera _camera;
        private AStarGridArray<AStarGridCellData> gridDataArray;
        private AStarGridArray<AStarGridItemUI> gridUIArray;
        private float gridWidth = 10f;

        private List<LineRenderer> lineRenderList = new List<LineRenderer>();
        //起始单元格
        private AStarGridCellData startCell;
        //结束单元格
        private AStarGridCellData endCell;

        #region 初始化
        private void Awake()
        {
            gridDataArray = new AStarGridArray<AStarGridCellData>(row, col);
            InitializeCellData();
            gridUIArray = new AStarGridArray<AStarGridItemUI>(row, col);
            InitializeCellUIItem();
            //初始化障碍物单元
            InitializeCellBlock();
        }

        void Start()
        {
            DrawGrid();
        }

        /// <summary>
        /// 初始化单元格数据
        /// </summary>
        public void InitializeCellData()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    AStarGridCellData data = new AStarGridCellData() { position = new Vector2Int(i, j) };
                    gridDataArray.SetData(i, j, data);
                }
            }
        }

        /// <summary>
        /// 初始化单元格UI项目
        /// </summary>
        private void InitializeCellUIItem()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Vector2 position = new Vector2(i * gridWidth + gridWidth / 2, j * gridWidth + gridWidth / 2);
                    var obj = Instantiate(gridPrefab, position, Quaternion.identity, this.transform);
                    var itemUI = obj.GetComponent<AStarGridItemUI>();
                    var cellData = gridDataArray.GetData(i, j);
                    itemUI.Data = cellData;
                    gridUIArray.SetData(i, j, itemUI);
                }
            }
        }

        /// <summary>
        /// 初始化障碍物单元
        /// </summary>
        private void InitializeCellBlock()
        {
            gridDataArray.GetData(2, 0).type = 3;
            gridDataArray.GetData(2, 1).type = 3;
            gridDataArray.GetData(2, 2).type = 3;
            gridDataArray.GetData(2, 3).type = 3;

            RefreshAllCellUI();
        }
        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //触发单元格点击事件处理
                var offest = Vector3.zero;//单元格偏移时的起始位置
                var cellPosition = GetMouseClickCellPosition(offest);
                if (!IsValidCellPosition(cellPosition)) return;
                CellClickHandler(cellPosition);
            }
        }

        #region Event Handler
        /// <summary>
        /// 元素点击
        /// </summary>
        /// <param name="obj"></param>
        private void CellClickHandler(Vector2Int position)
        {
            var data = gridUIArray.GetData(position.x, position.y);
            if (data == null) return;

            //障碍物点击无效
            if (data.Data.type == 3) return;

            //设置起始点
            if (startCell == null)
            {
                startCell = SetStartCell(data, 1);
                Debug.Log("设置起始点");
                RefreshAllCellUI();
                return;
            }

            //设置结束点
            if (endCell == null)
            {
                endCell = SetStartCell(data, 2);
                RefreshAllCellUI();
                Debug.Log("设置结束点");

                //已经设置了开始与结束点，开始寻路
                if (startCell != null && endCell != null)
                {
                    Debug.Log("开始寻路");
                    InitializePathFinding();
                }

                return;
            }
        }

        /// <summary>
        /// 设置开始节点
        /// </summary>
        /// <param name="clickItem"></param>
        private AStarGridCellData SetStartCell(AStarGridItemUI clickItem, int type)
        {
            var data = clickItem.Data;
            data.type = type;
            return data;
        }

        #endregion

        #region 元素绘制

        /// <summary>
        /// 绘制网格
        /// </summary>
        private void DrawGrid()
        {
            //绘制行网格线
            for (int i = 0; i <= row; i++)
            {
                Vector2 startPosition = new Vector2(0, i * gridWidth);
                Vector2 endPosition = new Vector2(col * gridWidth, i * gridWidth);
                CreateRenderLine(startPosition, endPosition, lineMaterial);
            }

            //绘制列网格线
            for (int i = 0; i <= col; i++)
            {
                Vector2 startPosition = new Vector2(i * gridWidth, 0);
                Vector2 endPosition = new Vector2(i * gridWidth, row * gridWidth);
                CreateRenderLine(startPosition, endPosition, lineMaterial);
            }
        }

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="lineMaterial"></param>
        /// <param name="lineWidth"></param>
        private void CreateRenderLine(Vector2 startPosition, Vector2 endPosition, Material lineMaterial, float lineWidth = 0.1f)
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
        private bool IsValidCellPosition(Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < row && position.y < col;
        }

        /// <summary>
        /// 获取鼠标点击的单元格
        /// </summary>
        private Vector2Int GetMouseClickCellPosition(Vector3 offest)
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - offest;//需要减去原点偏移
            int x = (int)(worldPosition.x / gridWidth);
            int y = (int)(worldPosition.y / gridWidth);
            var position = new Vector2Int(x, y);
            return position;
        }

        /// <summary>
        /// 刷新所有单元格
        /// </summary>
        private void RefreshAllCellUI()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var ui = gridUIArray.GetData(i, j);
                    ui.RefreshData();
                }
            }
        }
        #endregion

        #region 算法相关

        private List<AStarGridCellData> openList = new List<AStarGridCellData>();
        private List<AStarGridCellData> closeList = new List<AStarGridCellData>();

        //单元格八个方向
        private List<Vector2Int> cellNearAdjustmentList = new List<Vector2Int>()
        {
             new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1),
             new Vector2Int(-1,0), new Vector2Int(1,0),
             new Vector2Int(-1,-1), new Vector2Int(0,-1), new Vector2Int(1,-1),
        };

        /// <summary>
        /// 初始化寻路
        /// </summary>
        private void InitializePathFinding()
        {
            openList.Add(startCell);
            StartCoroutine(InitializePathFindingAsync());
        }

        /// <summary>
        /// 异步寻路
        /// </summary>
        private IEnumerator InitializePathFindingAsync()
        {
            yield return null;

            while (openList.Count > 0)
            {
                yield return new WaitForSeconds(2);

                var curCell = GetNearestCell(openList);
                if (curCell == null)
                    break;

                curCell.isSearchingCell = true;
                var result = OneStep(curCell);
                if (result)
                {
                    Debug.Log("找到路了");
                    curCell.isSearchingCell = false;
                    RefreshAllCellUI();
                    break;
                }

                curCell.isSearchingCell = false;

                RefreshAllCellUI();
            }

            yield return null;
        }

        /// <summary>
        /// 一次步骤
        /// </summary>
        private bool OneStep(AStarGridCellData curCell)
        {
            //判断当前节点8个方向,将其放入到开放节点中
            var nearList = GetNearCell(curCell);

            //当前节点向外扩张，可以放入已经处理的closeList中
            openList.Remove(curCell);

            curCell.isClosedCell = true;
            closeList.Add(curCell);


            //判断周围是否已经存在终点
            bool isMatchEnd = IsMatchEndCell(nearList);
            if (isMatchEnd)
            {
                //todo：记录结束节点
                return true;
            }

            //设置元素的g，h，f值
            foreach (var nearCell in nearList)
            {
                if (!openList.Contains(nearCell) && !closeList.Contains(nearCell))
                {
                    //设置g和h的值
                    nearCell.gCost = GetCellDistance(nearCell, startCell);
                    nearCell.hCost = GetCellDistance(nearCell, endCell);
                    openList.Add(nearCell);
                }
            }
            RefreshAllCellUI();

            return false;
        }

        /// <summary>
        /// 获取F数值最小的节点
        /// </summary>
        private AStarGridCellData GetNearestCell(List<AStarGridCellData> openList)
        {
            AStarGridCellData minDistanceCell = null;
            if (openList.Count <= 0) return minDistanceCell;

            minDistanceCell = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < minDistanceCell.fCost)
                    minDistanceCell = openList[i];
                else if (openList[i].fCost == minDistanceCell.fCost)
                {
                    //如果f相同则判断是否到终点的h值哪个最小
                    if (openList[i].hCost < minDistanceCell.hCost)
                        minDistanceCell = openList[i];
                }
            }
            return minDistanceCell;
        }

        /// <summary>
        /// 获取两个节点的距离
        /// </summary>
        private float GetCellDistance(AStarGridCellData originCell, AStarGridCellData targetCell)
        {
            var distance = Math.Round(Vector2Int.Distance(originCell.position, targetCell.position), 1);
            return (float)distance;
        }

        /// <summary>
        /// 获取临近的节点【周围8个单元格】
        /// </summary>
        /// <param name="cell"></param>
        private List<AStarGridCellData> GetNearCell(AStarGridCellData cell)
        {
            List<AStarGridCellData> nearList = new List<AStarGridCellData>();

            foreach (var adjustment in cellNearAdjustmentList)
            {
                int x = cell.position.x + adjustment.x;
                int y = cell.position.y + adjustment.y;

                if (!IsValidCellPosition(new Vector2Int(x, y)))
                    continue;

                var data = gridDataArray.GetData(x, y);
                //需要过滤障碍物
                if (data != null && data.type != 3)
                    nearList.Add(data);
            }

            return nearList;
        }

        /// <summary>
        /// 判断周围节点是否已经到达终点
        /// </summary>
        /// <param name="nearList"></param>
        /// <returns></returns>
        private bool IsMatchEndCell(List<AStarGridCellData> nearList)
        {
            return nearList.Contains(endCell);
        }
        #endregion
    }
}