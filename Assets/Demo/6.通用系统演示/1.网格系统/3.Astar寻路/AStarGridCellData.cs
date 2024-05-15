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
    public class AStarGridCellData : IComparable<AStarGridCellData>
    {
        public const int ObstacleDistance = 1000;//障碍物加权的距离【让障碍物在寻路时减低优先级】

        /// <summary>
        /// 节点索引位置
        /// </summary>
        public Vector2Int position;

        //gcost+hcost
        public float fCost { get { return gCost + hCost; } }

        //该格子到起点的位置
        public float gCost;

        //该格子到终点的位置
        public float hCost;

        //用于回溯节点
        public AStarGridCellData parentNode;


        //0:中间节点，1：起始点，2：结束点,3:障碍物
        public int type;

        //是否是已经搜索过的单元格
        public bool isClosedCell;

        //是否是正在搜索的单元格
        public bool isSearchingCell;

        public int CompareTo(AStarGridCellData other)
        {
            //如果当前Fcost小于其他则<0
            //如果当前Fcost大于其他则>0
            //如果当前Fcost与其他相同则=0

            int compare=fCost.CompareTo(other.fCost);
            if (compare != 0)
            {
                compare=hCost.CompareTo(other.hCost);
            }
            return compare;
        }
    }
}