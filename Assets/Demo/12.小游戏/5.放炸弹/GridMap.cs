using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.PlaceBomb
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private GameObject destoryableWallPrefab;//可摧毁墙体
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject bombPrefab;//炸弹

        [Header("地图属性")]
        private int row = 15;
        private int col = 15;
        private int gridSize = 2;
        private int[,] mapArray = null;
        private Vector3 girdStartPosition = Vector3.zero;

        private Camera _camera;

        #region 初始化

        void Start()
        {
            _camera = Camera.main;
            //girdStartPosition = new Vector3(-10, -10);

            InitializeGridMap();
            InitializeMapPrefab();
        }

        /// <summary>
        /// 初始化地图网格
        /// </summary>
        private void InitializeGridMap()
        {
            mapArray = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                    {
                        mapArray[i, j] = 2;//边界
                        continue;
                    }

                    var value = Random.Range(1, 10);
                    mapArray[i, j] = value <= 5 ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 初始化地图网格
        /// </summary>
        private void InitializeMapPrefab()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var pos = GetGridPosition(i, j);

                    if (mapArray[i, j] == 2)
                    {
                        Instantiate(wallPrefab, pos, Quaternion.identity, this.transform);
                    }
                    else if (mapArray[i, j] == 1)
                    {
                        Instantiate(destoryableWallPrefab, pos, Quaternion.identity, this.transform);
                    }
                }
            }
        }
        #endregion

        void Update()
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
            if (vec.x < 0 || vec.y < 0) return;

            //放置炸弹
            var pos = GetGridPosition(vec.x, vec.y);
            if (mapArray[vec.x, vec.y] == 0)
            {
                //只有空位置才能放置炸弹
                Instantiate(bombPrefab, pos, Quaternion.identity, this.transform);
            }
        }

        /// <summary>
        /// 获取网格位置
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGridPosition(int rowIndex, int colIndex)
        {
            return girdStartPosition + new Vector3(rowIndex * gridSize, colIndex * gridSize);
        }

        /// <summary>
        /// 获取点击的网格索引
        /// </summary>
        /// <returns></returns>
        private Vector2Int GetClickGridIndex()
        {
            var clickPos = _camera.ScreenToWorldPoint(Input.mousePosition) - girdStartPosition;

            //由于中心点问，需要偏移半个单元格
            int xIndex = (int)((clickPos.x + gridSize / 2) / gridSize);
            int yIndex = (int)((clickPos.y + gridSize / 2) / gridSize);

            //Debug.Log($"{clickPos}-{xIndex},{yIndex}");

            if (xIndex > row - 1 || yIndex > col - 1 || xIndex < 0 || yIndex < 0)
                return new Vector2Int(-1, -1);
            else
                return new Vector2Int(xIndex, yIndex);
        }
    }
}