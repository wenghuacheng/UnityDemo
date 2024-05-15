using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ·�����ƶ�
    /// </summary>
    public class WayPoint : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Vector3[] points;

        public Vector3[] Points => points;

        public Vector3 EntityPosition { get; set; }//������ʼλ��

        private bool gameStarted;

        private void Start()
        {
            EntityPosition = transform.position;
            gameStarted=true;//����ʱ���Ϊtrue������EntityPosition��һֱ�仯
        }

        /// <summary>
        /// ��ȡ��������
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
                //��Ϸû������ʱ��start�������ᱻ������������Ҫ������������ʼ�㣬����һֱʱ��0��0����
                EntityPosition = transform.position;
            }
        }
    }
}