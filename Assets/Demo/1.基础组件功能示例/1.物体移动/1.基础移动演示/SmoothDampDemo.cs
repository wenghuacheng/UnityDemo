using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class SmoothDampDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;
        //缓动时间，会影响启动与到达时。例如快到达目标时会减速，直到smoothTime后才会到达目标位置
        [SerializeField] float smoothTime = 3f;

        Vector3 velocity = Vector3.zero;//记录运动时间作为下一帧的速度

        private void FixedUpdate()
        {
            //MoveTowards的平滑方式
            //该方式不用考虑帧数带来的速度快慢问题          
            this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, target, ref velocity, smoothTime, speed);
        }
    }
}