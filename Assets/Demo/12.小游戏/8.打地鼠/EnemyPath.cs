using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Shoot2D
{
    /// <summary>
    /// 敌人移动路径
    /// </summary>
    public class EnemyPath
    {
        public List<Vector2> Positions = new List<Vector2>();

        //是否被占用
        public bool isUsed;

        //是否需要向着移动方向旋转
        public bool needRotation = true;

        public Vector2 GetStart()
        {
            if (Positions.Count <= 0) return Vector2.zero;
            return Positions[0];
        }

        public Vector2 GetEnd()
        {
            if (Positions.Count <= 1) return Vector2.zero;
            return Positions[1];
        }
    }
}