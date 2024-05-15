using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    [CreateAssetMenu(fileName = "ResourceList", menuName = "带UI演示/建造系统/资源信息列表")]
    public class ResourceListSO : ScriptableObject
    {
        public List<ResourceSO> list;
    }
}