using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectRotation
{
    public class QuaternionLookRotationDemo : MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update()
        {
            LookTarget();
        }

        private void LookTarget()
        {
            //方向向量
            var vectorToTarget = (target.transform.position - this.transform.position).normalized;
            //参数一可以理解为物体正视的方向【类比为人的眼睛，这样看向正前方】,或者理解为基于哪个轴旋转，这里是Z轴旋转
            //参数二可以理解为所看物体的方向【让自身通过旋转身体眼睛始终看向对方】
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
        }
    }
}
