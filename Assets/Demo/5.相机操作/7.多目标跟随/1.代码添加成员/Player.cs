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
            //添加玩家为追踪目标
            targetGroup.AddMember(this.transform, 1, 2);
            this.Invoke("AddMember", 2);//两秒后显示敌人和玩家
        }

        /// <summary>
        /// 添加成员
        /// </summary>
        private void AddMember()
        {
            //增加权重可以让相机进行特写
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