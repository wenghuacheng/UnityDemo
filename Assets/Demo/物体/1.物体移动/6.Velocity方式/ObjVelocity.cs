using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjVelocity : MonoBehaviour
    {
        private void FixedUpdate()
        {
            //让物体已持续速度运动
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right;
        }
    }
}