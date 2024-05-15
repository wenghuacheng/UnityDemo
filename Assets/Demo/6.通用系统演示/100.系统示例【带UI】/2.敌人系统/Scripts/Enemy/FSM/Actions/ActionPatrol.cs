using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// Ѳ���߼�
    /// </summary>
    public class ActionPatrol : FSMAction
    {
        [Header("Config")]
        [SerializeField] private float speed;

        private WayPoint wayPoint;
        private int pointIndex;

        private void Awake()
        {
            wayPoint = GetComponent<WayPoint>();
        }


        public override void Act()
        {
            FollowPath();
        }

        /// <summary>
        /// ����·����
        /// </summary>
        private void FollowPath()
        {
            var curPos = wayPoint.GetPosition(pointIndex);

            this.transform.position = Vector3.MoveTowards(transform.position, curPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, curPos) <= 0.1f)
            {
                UpdateNextPosition();
            }
        }

        /// <summary>
        /// ������һ�������λ��
        /// </summary>
        private void UpdateNextPosition()
        {
            pointIndex++;
            pointIndex = pointIndex % wayPoint.Points.Length;
        }
    }
}