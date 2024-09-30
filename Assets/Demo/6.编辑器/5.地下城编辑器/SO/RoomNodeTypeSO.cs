using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.DungeonEditor
{
    /// <summary>
    /// 房间节点类型
    /// 供节点在编辑器中选择
    /// </summary>
    [CreateAssetMenu(fileName = "RoomNodeType_", menuName = "带UI演示/地下城/房间类型")]
    public class RoomNodeTypeSO : ScriptableObject
    {
        public string roomName;

        //是否时通道类型
        public bool isCorridor;

        //是否是南北走向的走廊
        public bool isCorridorNS;

        //是否是东西走向的走廊
        public bool isCorridorEW;

        //是否是入口
        public bool isEntrance;

        //是否是房间类型
        public bool isRoom;

        //默认
        public bool isNone;

        //这里只是演示简化了
        ////是否是Boss房间
        //public bool isBossRoom;

        ////是否是小房间
        //public bool isSmallRoom;

        ////是否是中型房间
        //public bool isMediumRoom;

        ////是否是大型房间
        //public bool isBigRoom;
        
    }

}