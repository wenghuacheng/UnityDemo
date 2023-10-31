using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    /// <summary>
    /// 单个构造房间
    /// </summary>
    public class Room //: MonoBehaviour
    {
        //节点ID
        public string id;

        //父节点ID
        public string parentNodeId;

        //房间预制体
        public GameObject roomPrefab;

        //左下角边界坐标
        public Vector2Int lowerBound;

        //右上角边界坐标
        public Vector2Int upperBound;

        //模板的上下边界
        public Vector2Int templateLowerBounds;
        public Vector2Int templateUpperBounds;

        //门洞列表
        public List<RoomDoorway> doorList = new List<RoomDoorway>();
    }
}