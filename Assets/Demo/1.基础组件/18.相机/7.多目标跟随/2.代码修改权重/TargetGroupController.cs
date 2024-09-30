using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.CustomCamera.MultiTarget
{
    public class TargetGroupController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform enemy;

        [SerializeField] private CinemachineTargetGroup targetGroup;
               
        private float maxDistance = 20;//最远距离

        void Start()
        {
            //获取在CinemachineTargetGroup脚本中的对象
        }

        void Update()
        {
            var distance = Vector2.Distance(player.position, enemy.position);

            //通过修改权重实现player过于远离时将权重变成0
            //可以实现在接近敌人一定距离时让相机将敌人与玩家同时显示
            targetGroup.m_Targets[1].weight = Mathf.Clamp(1 - (distance / maxDistance), 0, 1);
        }
    }
}