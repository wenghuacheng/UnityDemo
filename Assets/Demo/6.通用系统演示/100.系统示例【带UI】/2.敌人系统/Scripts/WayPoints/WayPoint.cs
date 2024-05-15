using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 路径点移动
    /// </summary>
    public class WayPoint : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Vector3[] points;

        public Vector3[] Points => points;

        public Vector3 EntityPosition { get; set; }//人物起始位置

        private bool gameStarted;

        private void Start()
        {
            EntityPosition = transform.position;
            gameStarted=true;//启动时标记为true，否则EntityPosition会一直变化
        }

        /// <summary>
        /// 获取世界坐标
        /// </summary>
        /// <param name="pointIndex"></param>
        /// <returns></returns>
        public Vector3 GetPosition(int pointIndex)
        {
            return EntityPosition + points[pointIndex];
        }

        private void OnDrawGizmos()
        {
            if (!gameStarted && transform.hasChanged)
            {
                //游戏没有启动时，start函数不会被触发，所以需要在这里设置起始点，否则一直时（0，0）点
                EntityPosition = transform.position;
            }
        }
    }
}