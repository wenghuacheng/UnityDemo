using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    [CreateAssetMenu(fileName = "RoomNodeType_", menuName = "地下城/房间模板")]
    public class RoomTemplateSO : ScriptableObject
    {
        [HideInInspector] public string id;

        //房间预制体
        public GameObject roomPrefab;




        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
        }
    }
}