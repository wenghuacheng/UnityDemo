using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    [CreateAssetMenu(fileName = "Shape_", menuName = "形状匹配/形状")]
    public class MatchShape : ScriptableObject
    {
        public string id;

        public Sprite sprite;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
                Debug.Log($"创建id:{id}");
            }
        }
    }
}