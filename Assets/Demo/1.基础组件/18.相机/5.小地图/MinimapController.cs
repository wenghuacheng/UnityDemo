using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.MiniMap
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private GameObject minimapPlayer;

        //�����ͼ�����
        [SerializeField] private CinemachineVirtualCamera miniCamera;

        void Start()
        {
            //��������ĸ������
            miniCamera.Follow = player;
        }


        void Update()
        {
            if (player != null && minimapPlayer != null)
            {
                //��С��ͼ����ҵ�ͼ�������ʵ��λ��һ��
                minimapPlayer.transform.position = player.position;
            }
        }
    }
}
