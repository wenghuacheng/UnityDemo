using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.DungeonEditor
{
    [CreateAssetMenu(fileName = "RoomNodeTypeList_", menuName = "��UI��ʾ/���³�/���������б�")]
    public class RoomNodeTypeListSO : ScriptableObject
    {
        public List<RoomNodeTypeSO> list;
    }
}