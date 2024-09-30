using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Basic.Rb.CollisonDemo
{
    public class BoxCollider2DChecker : BaseCollider2DChecker
    {
        private BoxCollider2D _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<BoxCollider2D>();
        }

        protected override void CheckCollider()
        {
            var size = _collider.bounds.size;

            var s = Physics2D.OverlapBoxAll(this.transform.position, size, 0, layerMask);
            Debug.Log(s.Count() > 0 ? "重合了" : "不重合");
        }
    }
}