using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// 物体在网格中移动
    /// </summary>
    public class ObjectGridMoveDemo : BaseRenderGrid
    {
        [SerializeField] private Transform obj;

        private Vector3 targetPosition = Vector3.zero;
        private float speed = 5f;

        protected override void Update()
        {
            base.Update();

            if (Vector3.Distance(obj.position, targetPosition) >= 0.1)
            {
                var direction = targetPosition - obj.position;
                //Debug.Log($"obj:{obj.position},target:{targetPosition},direction:{direction}");
                obj.position += direction * speed * Time.deltaTime;
            }
        }

        protected override void CellClickHandler(Vector2Int position)
        {
            targetPosition = CalculateGridCenterPosition(position);
        }

        /// <summary>
        /// 计算网格中心点位置
        /// </summary>
        /// <param name="position"></param>
        private Vector3 CalculateGridCenterPosition(Vector2Int position)
        {
            var centerPosition = gridOffest + new Vector3(position.x * gridWidth + gridWidth / 2, position.y * gridWidth + gridWidth / 2);
            return centerPosition;
        }

    }
}