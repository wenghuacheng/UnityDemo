using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    //[CreateAssetMenu(fileName = "RoomDoorway_", menuName = "地下城/房间走廊")]
    [Serializable]
    public class RoomDoorway //: ScriptableObject
    {
        //走廊入口中心的坐标
        public Vector2Int entrancePosition;

        //左下角边界坐标
        public Vector2Int lowerBound;

        //右上角边界坐标
        public Vector2Int upperBound;

        //门洞的方向
        public DoorDirection direction;

        //是否已经连接
        public bool isConnected;
    }
}