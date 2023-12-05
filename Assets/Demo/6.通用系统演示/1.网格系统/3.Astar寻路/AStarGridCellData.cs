using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// AStar数据对象
    /// </summary>
    [Serializable]
    public class AStarGridCellData
    {
        /// <summary>
        /// 节点索引位置
        /// </summary>
        public Vector2Int position;

        //0:中间节点，1：起始点，2：结束点,3:障碍物
        public int type;

        //gcost+hcost
        public float fCost { get { return gCost + hCost; } }

        //该格子到起点的位置
        public float gCost;

        //该格子到终点的位置
        public float hCost;

        //是否是已经搜索过的单元格
        public bool isClosedCell;

        //是否是正在搜索的单元格
        public bool isSearchingCell;

        //父节点
        public AStarGridCellData parent;

    }
}