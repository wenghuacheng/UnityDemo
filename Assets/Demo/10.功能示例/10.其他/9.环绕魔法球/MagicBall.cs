using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.BallSurround
{
    public class MagicBall : MonoBehaviour
    {
        private int[] sortingLayers;
        private SpriteRenderer _renderer;
        private int index = 0;

        [SerializeField] private float height = 3f;//垂直方向移动半径
        [SerializeField] private float radius = 2f;//水平方向移动半径

        //X轴的移动方向【-1，1】（从右到左，从左到右）
        [SerializeField] private int directionX = 1;
        [SerializeField] private int offest = 0;


        private float speed = 3f;//小球速度     

        #region 初始化
        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();

            //设置切换的sort layer
            sortingLayers = new int[2];
            sortingLayers[0] = SortingLayer.NameToID("Background");
            sortingLayers[1] = SortingLayer.NameToID("Foreground");

            //让不同方向的初始层级不一样
            if (directionX == 1)
                index = 0;
            else if (directionX == -1)
                index = 1;
        }

        #endregion

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            var angle = Time.time * speed + offest;
            //通过三角函数计算水平位移【不适用Pingpong函数因为比较僵硬】
            float x = Mathf.Cos(angle) * radius;
            //float z = Mathf.Sin(angle) * radius;//z轴在3d下才能显示
            //计算垂直位移
            float y = Mathf.Cos(angle) * height;
            transform.position = new Vector3(x * directionX, y);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //切换排序层，让其旋转时一会在前面，一会在后面
                //这里时真的2d情况，3d情况修改Z轴即可实现
                index = (index + 1) % sortingLayers.Length;
                _renderer.sortingLayerID = sortingLayers[index];
            }
        }
    }
}