using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform firePosition;//����λ��
        [SerializeField] private Bullet bulletPrefab;//��ҩԤ����

        private Camera mainCamera;
        private float width;//��ǰ��ҿ��
        private float playerSpeed = 10;

        //��Ļ�����������
        private Vector2 leftTopPos;
        private Vector2 rightTopPos;
        private float playerMinX = 0;
        private float playerMaxX = 0;

        //��ҩ����
        private float maxFireTime = 0.2f;
        private float fireTime = 0;

        #region ��ʼ��
        void Start()
        {
            mainCamera = Camera.main;

            InitilizeMovementArea();
        }

        /// <summary>
        /// ��ʼ������ƶ�����[����]
        /// </summary>
        private void InitilizeMovementArea()
        {
            var renderer = GetComponent<SpriteRenderer>();
            width = renderer.bounds.size.x;

            leftTopPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1));
            rightTopPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));

            playerMinX = leftTopPos.x + width / 2;
            playerMaxX = rightTopPos.x - width / 2;
        }

        #endregion

        void Update()
        {
            Movement();
            Fire();
        }

        #region ����ƶ�
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            this.transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime * playerSpeed);

            var newX = Mathf.Clamp(this.transform.position.x, playerMinX, playerMaxX);
            this.transform.position = new Vector3(newX, this.transform.position.y);
        }
        #endregion

        #region ���䵯ҩ
        private void Fire()
        {
            fireTime -= Time.deltaTime;

            if (!Input.GetMouseButtonDown(0)) return;
            if (fireTime >= 0) return;

            fireTime = maxFireTime;

            var bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity, null);
            bullet.SetDirection(this.transform.up);
        }

        #endregion
    }
}
