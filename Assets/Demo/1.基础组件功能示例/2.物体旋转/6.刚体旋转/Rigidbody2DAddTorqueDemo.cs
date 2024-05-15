using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    public class Rigidbody2DAddTorqueDemo : MonoBehaviour
    {
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddTorque(30);
        }

        private void FixedUpdate()
        {
            ////不停调用则速度会不停的添加
            ////除非存在阻尼
            //rb.AddTorque(1);
        }
    }
}