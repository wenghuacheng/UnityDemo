using Demo.DungeonEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    [CreateAssetMenu(fileName = "RoomTemplate_", menuName = "��UI��ʾ/���³�/����ģ��")]
    public class RoomTemplateSO : ScriptableObject
    {
        //[HideInInspector] public string id;

        //����Ԥ����
        public GameObject roomPrefab;

        //���½Ǳ߽�����
        public Vector2Int lowerBound;

        //���ϽǱ߽�����
        public Vector2Int upperBound;

        //�Ŷ��б�
        [SerializeField] public List<RoomDoorway> doorList;

        //��������
        public RoomNodeTypeSO roomNodeType;

        //private void OnValidate()
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //        id = Guid.NewGuid().ToString();
        //}
    }
}