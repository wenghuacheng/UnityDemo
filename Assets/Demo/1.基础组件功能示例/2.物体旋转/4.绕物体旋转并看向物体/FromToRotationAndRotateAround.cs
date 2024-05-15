using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// ����Ŀ�겢��ת
    /// </summary>
    public class FromToRotationAndRotateAround : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 30;

        void Update()
        {
            //��������
            var vectorToTarget = (target.transform.position - this.transform.position).normalized;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vectorToTarget);//����Ϊ�����ĳ���

            //����Ŀ����ת
            this.transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
