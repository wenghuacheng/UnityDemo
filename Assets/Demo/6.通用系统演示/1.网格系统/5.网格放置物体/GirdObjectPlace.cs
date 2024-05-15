using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Common.Grids
{
    public class GirdObjectPlace : BaseRenderGrid
    {
        [SerializeField] private Building[] buildingList;
        private Building curBuildPrefab;
        private Building curBuild;

        private int[,] gridPlace;//已放置网格的位置

        #region Initialize
        protected override void Awake()
        {
            gridPlace = new int[row, col];

            base.Awake();
            InitializeBuild();
        }

        private void InitializeBuild()
        {
            curBuildPrefab = buildingList.FirstOrDefault();
            curBuild = Instantiate(curBuildPrefab);
        }
        #endregion

        protected override void Update()
        {
            base.Update();
            SwitchBuilding();
            var cellPosition = PrePlaceBuild();
            PlaceBuild(cellPosition);
        }

        #region 建筑网格选择

        /// <summary>
        /// 切换建筑
        /// </summary>
        private void SwitchBuilding()
        {
            bool isChanged = false;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                curBuildPrefab = buildingList[0];
                isChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                curBuildPrefab = buildingList[1];
                isChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                curBuildPrefab = buildingList[2];
                isChanged = true;
            }

            if (isChanged)
            {
                if (curBuild != null)
                {
                    Destroy(curBuild.gameObject);
                }
                curBuild = null;
                curBuild = Instantiate(curBuildPrefab);
            }
        }

        /// <summary>
        /// 预放置建筑
        /// </summary>
        private Vector2Int PrePlaceBuild()
        {
            var cellPosition = GetMouseCellPosition();
            bool bl = IsValidCellPosition(cellPosition);
            if (!bl) return new Vector2Int(-1, -1);

            if (curBuild != null)
                curBuild.transform.position = GetGridWorldPostion(cellPosition);

            return cellPosition;
        }

        #endregion

        #region 建筑放置

        private bool CanBuild(Vector2Int clickCellPosition)
        {
            var size = curBuild.size;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var position = new Vector2Int(clickCellPosition.x + j, clickCellPosition.y + i);
                    if (gridPlace[position.x, position.y] == 1)
                    {
                        Debug.Log("地块已经被占用");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 放置建筑
        /// </summary>
        private void PlaceBuild(Vector2Int clickCellPosition)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!IsValidCellPosition(clickCellPosition) || curBuild == null || !CanBuild(clickCellPosition)) return;

            var size = curBuild.size;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var position = new Vector2Int(clickCellPosition.x + j, clickCellPosition.y + i);
                    gridPlace[position.x, position.y] = 1;
                }
            }
            //在当前位置生成一个物体
            var initializePosition = GetGridWorldPostion(clickCellPosition);
            Instantiate(curBuildPrefab, initializePosition, Quaternion.identity);
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 获取当前单元格的世界坐标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected Vector2 GetGridWorldPostion(Vector2Int position)
        {
            ////建筑的定位点在中心
            //return new Vector2(position.x * gridWidth + gridWidth / 2, position.y * gridWidth + gridWidth / 2);
            //建筑的定位点在左下角
            return new Vector2(position.x * gridWidth, position.y * gridWidth);
        }
        #endregion
    }
}