using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    [CreateAssetMenu(fileName = "Shape_", menuName = "С��Ϸ/ƥ����Ϸ/��״")]
    public class MatchShape : ScriptableObject
    {
        public string id;

        public Sprite sprite;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
                Debug.Log($"����id:{id}");
            }
        }
    }
}