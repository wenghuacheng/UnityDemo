using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.ObjectMove.FormationMovement
{
    /// <summary>
    /// 编队
    /// </summary>
    public class Formation : MonoBehaviour
    {
        [SerializeField] private SingleItem[] members;//编队成员
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
            //计算整个队列的区域
            var area = CalculateFormationArea();
            //获取移动定位锚点
            var anchorPoint = GetAnchorPoint(targetPosition);
            //设置成员目标点
            SetMemberTarget(targetPosition, anchorPoint);
        }

        /// <summary>
        /// 计算整个队列的区域
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
        /// 获取移动定位锚点
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
        /// 设置成员目标点
        /// </summary>
        private void SetMemberTarget(Vector3 targetPosition, Vector3 anchorPoint)
        {
            //目标与锚点的间距
            var offestVector = targetPosition - anchorPoint;

            //基于锚点计算出每个成员的目标位置
            foreach (var member in members)
            {
                var target = member.transform.position + offestVector;
                member.SetTarget(_speed, target);
            }
        }
    }
}