using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.GoGrid
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private int cellCount = 40;//单元格数量
        [SerializeField] private int maxHorizontalCell = 11;//最大的水平单元格数量
        [SerializeField] private int maxVerticalCell = 2;//最大的垂直单元格数量
        [SerializeField] private Cell cellPrefab;//地块预制体

        private float cellSize = 5;//单元格尺寸 
        private List<Vector3> cellPosList = new List<Vector3>();//单元格位置列表
        private List<Cell> cellVisualList = new List<Cell>();//单元格显示元素列表

        #region 初始化
        private void Awake()
        {
            InitializeGrid();
            CenterMap();
            InitializeGridVisual();
            RotateDirection();

            //StartCoroutine(AutoSwitchCellColor());
        }

        /// <summary>
        /// 生成网格的位置
        /// </summary>
        private void InitializeGrid()
        {
            int remainCount = cellCount;
            int curHorizontalIndex = 0;//当前水平方向生成索引
            int curVerticalIndex = 0;//当前垂直方向生成索引

            int stepVerticalCount = 0;//一个阶段已生成垂直方向格子数量

            int index = 0;//生成方向索引，0从左到右，1向下，2从右向左，3向下

            while (remainCount > 0)
            {
                Vector2 pos = new Vector2(curHorizontalIndex * cellSize, -curVerticalIndex * cellSize);
                cellPosList.Add(pos);

                remainCount--;

                //计算下一个位置
                index = index % 4;//以4个数为一个循环
                if (index == 0)//从左向右生成
                {
                    #region 从左向右生成
                    if (curHorizontalIndex >= maxHorizontalCell)
                    {
                        //换行
                        curVerticalIndex++;
                        index++;
                        stepVerticalCount = 0;
                    }
                    else
                    {
                        //向右延展
                        curHorizontalIndex++;
                    }
                    #endregion
                }
                else if (index == 1 || index == 3)//向下生成
                {
                    #region 向下生成
                    if (stepVerticalCount >= maxVerticalCell - 1)
                    {
                        index++;
                    }
                    curVerticalIndex++;
                    stepVerticalCount++;
                    #endregion
                }
                else if (index == 2)//从右向左生成
                {
                    #region 从右向左生成
                    if (curHorizontalIndex <= 0)
                    {
                        //换行
                        curVerticalIndex++;
                        index++;
                        stepVerticalCount = 0;
                    }
                    else
                    {
                        //向左延展
                        curHorizontalIndex--;
                    }
                    #endregion
                }

            }
        }

        /// <summary>
        /// 生成网格的位置
        /// </summary>
        private void InitializeGridVisual()
        {
            for (int i = 0; i < cellPosList.Count; i++)
            {
                var obj = Instantiate(cellPrefab, cellPosList[i], Quaternion.identity, this.transform);
                obj.transform.localScale = new Vector3(cellSize * 0.9f, cellSize * 0.9f, 1);//缩小一点，别贴着
                cellVisualList.Add(obj);
            }
        }

        /// <summary>
        /// 让地图居中
        /// </summary>
        private void CenterMap()
        {
            var minX = cellPosList.Select(p => p.x).Min();
            var maxX = cellPosList.Select(p => p.x).Max();
            var minY = cellPosList.Select(p => p.y).Min();
            var maxY = cellPosList.Select(p => p.y).Max();

            var offest = new Vector3(Mathf.Abs((maxX - minX) / 2), -Mathf.Abs((maxY - minY) / 2));

            for (int i = 0; i < cellPosList.Count; i++)
            {
                cellPosList[i] = cellPosList[i] - offest;
            }
        }

        /// <summary>
        /// 旋转指示方向
        /// </summary>
        private void RotateDirection()
        {
            for (int i = 1; i < cellVisualList.Count; i++)
            {
                var prevObj = cellVisualList[i - 1];
                var curObj = cellVisualList[i];

                //旋转地块，让箭头指示下一个位置
                var direction = (curObj.transform.position - prevObj.transform.position).normalized;
                prevObj.transform.up = direction;
            }

            //最后一个使用前面的旋转方向
            cellVisualList[cellVisualList.Count - 1].transform.up = cellVisualList[cellVisualList.Count - 2].transform.up;
        }
        #endregion

        #region 属性
        public List<Vector3> CellPositionList { get { return cellPosList; } }
        #endregion

        /// <summary>
        /// 切换单元格的颜色
        /// </summary>
        private IEnumerator AutoSwitchCellColor()
        {
            int curIndex = 0;
            int prevIndex = 0;
            while (true)
            {
                cellVisualList[prevIndex].SetStatus(0);
                cellVisualList[curIndex].SetStatus(1);

                yield return new WaitForSeconds(0.5f);

                prevIndex = curIndex;
                curIndex = (curIndex + 1) % cellVisualList.Count;
            }
        }
    }
}