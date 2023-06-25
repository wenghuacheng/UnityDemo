using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveSmoothDamp : MonoBehaviour
{
    private float speed = 1;
    private Vector3 target = new Vector3(5, 0, 0);
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.5f;
   
    void Update()
    {
        //MoveTowards的平滑方式
        //该方式不用考虑帧数带来的速度快慢问题
        this.transform.position = Vector3.SmoothDamp(this.transform.position, target,ref velocity, smoothTime, speed);
    }
}
