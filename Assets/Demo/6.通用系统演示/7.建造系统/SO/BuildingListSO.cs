using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    [CreateAssetMenu(fileName = "BuildingList", menuName = "��UI��ʾ/����ϵͳ/������Ϣ�б�")]
    public class BuildingListSO : ScriptableObject
    {
        public List<BuildingSO> list;
    }
}