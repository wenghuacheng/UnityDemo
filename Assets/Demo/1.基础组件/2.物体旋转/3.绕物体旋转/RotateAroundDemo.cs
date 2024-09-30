using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectRotation
{
    public class RotateAroundDemo : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;

        void Update()
        {
            //Vector3.forward即绕Z轴旋转
            this.transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
