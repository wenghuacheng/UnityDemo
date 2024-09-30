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

        //��Ļ�����������
        private Vector2 leftTopPos;
        private Vector2 rightTopPos;
        private Vector2 leftBottomPos;
        private Vector2 rightBottomPos;

        //����
        private float playerMinX = 0;
        private float playerMaxX = 0;
        private float playerMinY = 0;
        private float playerMaxY = 0;

        #region ��ʼ��
        private void Awake()
        {
            mainCamera = Camera.main;
            InitilizeCameraArea();
            InitilizePlayerArea();
            InitilizePlayerDefaultPosition();
        }

        /// <summary>
        /// ��ȡ��Ļ��������
        /// </summary>
        private void InitilizeCameraArea()
        {
            leftTopPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1));
            rightTopPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));
            leftBottomPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));
            rightBottomPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0));
        }

        /// <summary>
        /// ��ʼ����һ����
        /// </summary>
        private void InitilizePlayerArea()
        {
            var renderer = GetComponent<SpriteRenderer>();
            width = renderer.bounds.size.x;
            height = renderer.bounds.size.y;

            //������Ҷ�λ��Ϊ���ĵ㣬������Ҫ����߽�
            playerMinX = leftTopPos.x + width / 2;
            playerMaxX = rightTopPos.x - width / 2;
            playerMinY = leftBottomPos.y + height / 2;
            playerMaxY = leftTopPos.y - height / 2;
        }

        /// <summary>
        /// ��ʼ����ҵ�Ĭ��λ�á���������Ǳͧ��սʱ�����ĳ��λ���ƶ���
        /// </summary>
        private void InitilizePlayerDefaultPosition()
        {
            var rect = bg.GetComponent<RectTransform>();
            var centerX = (playerMaxX + playerMinX) / 2;

            //���㵱ǰ�����ĳߴ���������Ļ�ı���
            var scale = rect.rect.height / (float)Screen.height;

            //ͨ��ռ�ñ���������������λ��
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
        /// �ƶ�
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