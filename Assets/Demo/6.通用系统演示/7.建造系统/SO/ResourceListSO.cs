using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    [CreateAssetMenu(fileName = "ResourceList", menuName = "��UI��ʾ/����ϵͳ/��Դ��Ϣ�б�")]
    public class ResourceListSO : ScriptableObject
    {
        public List<ResourceSO> list;
    }
}