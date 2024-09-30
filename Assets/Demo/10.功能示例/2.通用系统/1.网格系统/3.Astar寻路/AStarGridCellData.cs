using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// AStar���ݶ���
    /// </summary>
    [Serializable]
    public class AStarGridCellData : IComparable<AStarGridCellData>
    {
        public const int ObstacleDistance = 1000;//�ϰ����Ȩ�ľ��롾���ϰ�����Ѱ·ʱ�������ȼ���

        /// <summary>
        /// �ڵ�����λ��
        /// </summary>
        public Vector2Int position;

        //gcost+hcost
        public float fCost { get { return gCost + hCost; } }

        //�ø��ӵ�����λ��
        public float gCost;

        //�ø��ӵ��յ��λ��
        public float hCost;

        //���ڻ��ݽڵ�
        public AStarGridCellData parentNode;


        //0:�м�ڵ㣬1����ʼ�㣬2��������,3:�ϰ���
        public int type;

        //�Ƿ����Ѿ��������ĵ�Ԫ��
        public bool isClosedCell;

        //�Ƿ������������ĵ�Ԫ��
        public bool isSearchingCell;

        public int CompareTo(AStarGridCellData other)
        {
            //�����ǰFcostС��������<0
            //�����ǰFcost����������>0
            //�����ǰFcost��������ͬ��=0

            int compare=fCost.CompareTo(other.fCost);
            if (compare != 0)
            {
                compare=hCost.CompareTo(other.hCost);
            }
            return compare;
        }
    }
}