using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.DungeonEditor
{
    [CreateAssetMenu(fileName = "RoomNodeTypeList_", menuName = "地下城/房间类型列表")]
    public class RoomNodeTypeListSO : ScriptableObject
    {
        public List<RoomNodeTypeSO> list;
    }
}