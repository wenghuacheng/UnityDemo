using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Basic.Rb.CollisonDemo
{
    public class CircleCollider2DChecker : BaseCollider2DChecker
    {
        private CircleCollider2D _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CircleCollider2D>();
        }

        protected override void CheckCollider()
        {
            var s = Physics2D.OverlapCircleAll(this.transform.position, _collider.radius, layerMask);
            Debug.Log(s.Count() > 0 ? "重合了" : "不重合");
        }
    }
}
