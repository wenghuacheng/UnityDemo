using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.CustomCamera.CameraAreaMove
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Image bg;

        private Camera mainCamera;

        private float speed = 10;
        private float width = 0;
        private float height = 0;

        //屏幕区域相关坐标
        private Vector2 leftTopPos;
        private Vector2 rightTopPos;
        private Vector2 leftBottomPos;
        private Vector2 rightBottomPos;

        //区域
        private float playerMinX = 0;
        private float playerMaxX = 0;
        private float playerMinY = 0;
        private float playerMaxY = 0;

        #region 初始化
        private void Awake()
        {
            mainCamera = Camera.main;
            InitilizeCameraArea();
            InitilizePlayerArea();
            InitilizePlayerDefaultPosition();
        }

        /// <summary>
        /// 获取屏幕区域坐标
        /// </summary>
        private void InitilizeCameraArea()
        {
            leftTopPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1));
            rightTopPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));
            leftBottomPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));
            rightBottomPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0));
        }

        /// <summary>
        /// 初始化玩家活动区域
        /// </summary>
        private void InitilizePlayerArea()
        {
            var renderer = GetComponent<SpriteRenderer>();
            width = renderer.bounds.size.x;
            height = renderer.bounds.size.y;

            //由于玩家定位点为中心点，所以需要计算边界
            playerMinX = leftTopPos.x + width / 2;
            playerMaxX = rightTopPos.x - width / 2;
            playerMinY = leftBottomPos.y + height / 2;
            playerMaxY = leftTopPos.y - height / 2;
        }

        /// <summary>
        /// 初始化玩家的默认位置【测试例如潜艇大战时玩家在某个位置移动】
        /// </summary>
        private void InitilizePlayerDefaultPosition()
        {
            var rect = bg.GetComponent<RectTransform>();
            var centerX = (playerMaxX + playerMinX) / 2;

            //计算当前背景的尺寸与整个屏幕的比例
            var scale = rect.rect.height / (float)Screen.height;

            //通过占用比例计算出玩家所在位置
            var allHeight = leftTopPos.y - leftBottomPos.y;
            var yPos = leftBottomPos.y + allHeight * scale + height / 2;
            this.transform.position = new Vector3(centerX, yPos);
        }
        #endregion


        private void Update()
        {
            Movement();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            this.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);

            var newX = Mathf.Clamp(this.transform.position.x, playerMinX, playerMaxX);
            var newY = Mathf.Clamp(this.transform.position.y, playerMinY, playerMaxY);

            this.transform.position = new Vector3(newX, newY, this.transform.position.z);
        }
    }
}