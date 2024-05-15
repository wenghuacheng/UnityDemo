using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Shoot2D
{
    /// <summary>
    /// 障碍物
    /// </summary>
    public class Block : MonoBehaviour
    {
        //生成位置
        [SerializeField] private Transform spawnerPositionParent;

        public List<EnemyPath> pathList = new List<EnemyPath>();

        private void Awake()
        {
            GetSpawnerPostions();
        }

        /// <summary>
        /// 获取所有的生成点
        /// </summary>
        private void GetSpawnerPostions()
        {
            for (int i = 0; i < spawnerPositionParent.childCount; i++)
            {
                var child = spawnerPositionParent.GetChild(i);

                EnemyPath path = new EnemyPath();
                for (int j = 0; j < child.childCount; j++)
                {
                    var p = child.GetChild(j);
                    path.Positions.Add(p.position);
                }
                pathList.Add(path);
            }
        }
    }
}