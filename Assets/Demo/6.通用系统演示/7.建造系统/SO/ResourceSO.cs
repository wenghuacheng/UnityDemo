using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// 资源信息
    /// </summary>
    [CreateAssetMenu(fileName = "Resource_", menuName = "带UI演示/建造系统/资源信息")]
    public class ResourceSO : ScriptableObject
    {
        public string description;

        ////资源图标【演示就使用描述】
        //public Sprite icon;
    }
}