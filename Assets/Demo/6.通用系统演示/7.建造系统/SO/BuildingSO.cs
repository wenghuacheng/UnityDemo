using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    [CreateAssetMenu(fileName = "Building_", menuName = "��UI��ʾ/����ϵͳ/������Ϣ")]
    public class BuildingSO : ScriptableObject
    {
        public Building prefab;

        public BuildingGhost ghostPrefab;//Ԥ����

        public string description;

        public float maxResGathering;//�����ռ�ʱ��

        public ResourceSO res;//��ǰ������������Դ

        public int resCount;//���β�����Դ����

        public BuildingCost[] cost;//�����Ĳ�
    }
}