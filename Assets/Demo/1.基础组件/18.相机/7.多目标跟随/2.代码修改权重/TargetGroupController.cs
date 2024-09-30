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
               
        private float maxDistance = 20;//��Զ����

        void Start()
        {
            //��ȡ��CinemachineTargetGroup�ű��еĶ���
        }

        void Update()
        {
            var distance = Vector2.Distance(player.position, enemy.position);

            //ͨ���޸�Ȩ��ʵ��player����Զ��ʱ��Ȩ�ر��0
            //����ʵ���ڽӽ�����һ������ʱ����������������ͬʱ��ʾ
            targetGroup.m_Targets[1].weight = Mathf.Clamp(1 - (distance / maxDistance), 0, 1);
        }
    }
}