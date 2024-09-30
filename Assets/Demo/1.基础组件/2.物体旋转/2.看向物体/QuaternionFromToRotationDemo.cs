using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectRotation
{
    public class QuaternionFromToRotationDemo: MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update()
        {
            LookTarget();
        }

        private void LookTarget()
        {
            var vectorToTarget = target.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vectorToTarget);//参数为基础的朝向
        }
    }
}
