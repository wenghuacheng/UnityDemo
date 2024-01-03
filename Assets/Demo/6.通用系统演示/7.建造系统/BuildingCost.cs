using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// 建筑消耗
    /// </summary>
    [Serializable]
    public class BuildingCost
    {
        //资源类型
        public ResourceSO res;

        //资源数量
        public int count;
    }
}