using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraAfterTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            //在LateUpdate中移动可以解决抖动问题
            CameraSmoothDampMove02();
        }

        /// <summary>
        /// 当超过一定的移动范围，相机才跟随
        /// </summary>
        private void CameraSmoothDampMove02()
        {
            Vector3 velocity = Vector3.zero;

            var distance = Vector2.Distance(this.transform.position, target.position);

            //超过距离才移动
            if (distance > 1f)
            {
                this.transform.position = Vector3.SmoothDamp(this.transform.position
                    ,new Vector3(target.position.x, target.position.y, this.transform.position.z), ref velocity, 0.06f);
            }
        }
    }
}