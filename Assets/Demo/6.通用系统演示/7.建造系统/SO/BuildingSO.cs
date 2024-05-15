using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// 建筑信息
    /// </summary>
    [CreateAssetMenu(fileName = "Building_", menuName = "带UI演示/建造系统/建筑信息")]
    public class BuildingSO : ScriptableObject
    {
        public Building prefab;

        public BuildingGhost ghostPrefab;//预建造

        public string description;

        public float maxResGathering;//单次收集时间

        public ResourceSO res;//当前建筑产生的资源

        public int resCount;//单次产生资源数量

        public BuildingCost[] cost;//建筑耗材
    }
}