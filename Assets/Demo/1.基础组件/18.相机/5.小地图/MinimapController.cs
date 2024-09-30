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

        //迷你地图摄像机
        [SerializeField] private CinemachineVirtualCamera miniCamera;

        void Start()
        {
            //设置相机的跟随对象
            miniCamera.Follow = player;
        }


        void Update()
        {
            if (player != null && minimapPlayer != null)
            {
                //让小地图上玩家的图标与玩家实际位置一致
                minimapPlayer.transform.position = player.position;
            }
        }
    }
}
