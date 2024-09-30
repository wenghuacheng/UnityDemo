using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.ObjectMove.FormationMovement
{
    /// <summary>
    /// ���
    /// </summary>
    public class Formation : MonoBehaviour
    {
        [SerializeField] private SingleItem[] members;//��ӳ�Ա
        [SerializeField] private Camera _camera;
        private float _speed = 5;

        void Update()
        {
            MouseDown();
        }

        private void MouseDown()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            //�����������е�����
            var area = CalculateFormationArea();
            //��ȡ�ƶ���λê��
            var anchorPoint = GetAnchorPoint(targetPosition);
            //���ó�ԱĿ���
            SetMemberTarget(targetPosition, anchorPoint);
        }

        /// <summary>
        /// �����������е�����
        /// </summary>
        private Tuple<Vector2, Vector2> CalculateFormationArea()
        {
            var points = members.Select(p => p.transform.position);
            var xList = points.Select(p => p.x);
            var yList = points.Select(p => p.y);

            var minX = xList.Min();
            var maxX = xList.Max();
            var minY = yList.Min();
            var maxY = yList.Max();

            return new Tuple<Vector2, Vector2>(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        /// <summary>
        /// ��ȡ�ƶ���λê��
        /// </summary>
        private Vector3 GetAnchorPoint(Vector3 targetPosition)
        {
            SingleItem minDistanceMember = members[0];
            float minDistance = Vector2.Distance(targetPosition, minDistanceMember.transform.position);
            for (int i = 1; i < members.Count(); i++)
            {
                var distance = Vector2.Distance(targetPosition, members[i].transform.position);
                if (distance < minDistance)
                {
                    minDistanceMember = members[i];
                    minDistance = distance;
                }
            }

            return minDistanceMember.transform.position;
        }

        /// <summary>
        /// ���ó�ԱĿ���
        /// </summary>
        private void SetMemberTarget(Vector3 targetPosition, Vector3 anchorPoint)
        {
            //Ŀ����ê��ļ��
            var offestVector = targetPosition - anchorPoint;

            //����ê������ÿ����Ա��Ŀ��λ��
            foreach (var member in members)
            {
                var target = member.transform.position + offestVector;
                member.SetTarget(_speed, target);
            }
        }
    }
}