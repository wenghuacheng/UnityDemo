using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    [CreateAssetMenu(fileName = "BuildingList", menuName = "建造系统/建筑信息列表")]
    public class BuildingListSO : ScriptableObject
    {
        public List<BuildingSO> list;
    }
}