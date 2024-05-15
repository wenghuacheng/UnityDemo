using Demo.DungeonEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    [CreateAssetMenu(fileName = "RoomTemplate_", menuName = "带UI演示/地下城/房间模板")]
    public class RoomTemplateSO : ScriptableObject
    {
        //[HideInInspector] public string id;

        //房间预制体
        public GameObject roomPrefab;

        //左下角边界坐标
        public Vector2Int lowerBound;

        //右上角边界坐标
        public Vector2Int upperBound;

        //门洞列表
        [SerializeField] public List<RoomDoorway> doorList;

        //房间类型
        public RoomNodeTypeSO roomNodeType;

        //private void OnValidate()
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //        id = Guid.NewGuid().ToString();
        //}
    }
}