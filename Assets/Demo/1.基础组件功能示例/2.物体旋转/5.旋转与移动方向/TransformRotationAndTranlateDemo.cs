using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// 旋转影响前进方向
    /// </summary>
    public class TransformRotationAndTranlateDemo : MonoBehaviour
    {
        void Update()
        {
            //旋转时物体的坐标系也会相应旋转
            this.transform.Rotate(0, 0, 45 * Time.deltaTime);
            //向着物体的上方向移动
            this.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
}