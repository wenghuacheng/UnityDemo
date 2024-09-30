using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectMove
{
    public class AddForceDemo: MonoBehaviour
    {
        [SerializeField] private float force = 1;
        [SerializeField] private Vector3 target;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = this.transform.GetComponent<Rigidbody2D>();

            //在有阻尼的情况下会慢慢停下来，如果需要持续移动则需要在Update中持续加力
            //只能针对dynamic类型，其他类型的物体不会动
            rb.AddForce(Vector2.right * force);
        }

        private void FixedUpdate()
        {
            //到达位置后停止
            if (this.transform.localPosition.x > target.x)
            {
                rb.velocity = Vector2.zero;
                return;
            }
        }
    }
}
