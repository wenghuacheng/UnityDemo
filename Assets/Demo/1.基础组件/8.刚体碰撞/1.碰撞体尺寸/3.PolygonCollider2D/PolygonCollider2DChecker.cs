using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Basic.Rb.CollisonDemo
{
    public class PolygonCollider2DChecker : BaseCollider2DChecker
    {
        private PolygonCollider2D _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<PolygonCollider2D>();
        }

        protected override void CheckCollider()
        {
            var points = _collider.points;

            // 检查每个顶点是否在第二个多边形内部
            foreach (Vector2 point in points)
            {
                Vector2 worldPoint = _collider.transform.TransformPoint(point);
                var targetColliders = Physics2D.OverlapPointAll(worldPoint, layerMask);

                if (targetColliders.Count() > 0)
                {
                    Debug.Log("重合");
                    return;
                }
            }
            Debug.Log("不重合");
        }
    }
}
