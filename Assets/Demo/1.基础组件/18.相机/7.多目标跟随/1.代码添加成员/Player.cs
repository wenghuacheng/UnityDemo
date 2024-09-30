using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Demo.CustomCamera.MultiTarget
{
    public class Player : MonoBehaviour
    {
        private float speed = 5;

        [SerializeField] private CinemachineTargetGroup targetGroup;
        [SerializeField] private Transform target;

        void Start()
        {
            //������Ϊ׷��Ŀ��
            targetGroup.AddMember(this.transform, 1, 2);
            this.Invoke("AddMember", 2);//�������ʾ���˺����
        }

        /// <summary>
        /// ��ӳ�Ա
        /// </summary>
        private void AddMember()
        {
            //����Ȩ�ؿ��������������д
            targetGroup.AddMember(target, 1, 2);
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            this.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);
        }
    }
}