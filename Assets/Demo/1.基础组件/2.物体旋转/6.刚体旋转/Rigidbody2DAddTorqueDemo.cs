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
            ////��ͣ�������ٶȻ᲻ͣ�����
            ////���Ǵ�������
            //rb.AddTorque(1);
        }
    }
}