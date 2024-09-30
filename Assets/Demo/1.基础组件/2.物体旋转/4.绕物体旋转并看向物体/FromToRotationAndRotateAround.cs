using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// 看向目标并旋转
    /// </summary>
    public class FromToRotationAndRotateAround : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 30;

        void Update()
        {
            //看向物体
            var vectorToTarget = (target.transform.position - this.transform.position).normalized;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vectorToTarget);//参数为基础的朝向

            //绕着目标旋转
            this.transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
