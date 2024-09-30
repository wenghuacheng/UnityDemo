using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectMove
{
    public class VelocityDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = this.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(speed, 0);
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
