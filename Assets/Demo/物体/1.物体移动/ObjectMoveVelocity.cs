using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveVelocity : MonoBehaviour
{
    private void FixedUpdate()
    {
        //让物体已持续速度运动
        this.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right;
    }
}
