using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Shoot
{
    /// <summary>
    /// �����ű�
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform shootPosition;

        private Camera _camera;

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            AimToMousePosition();

            if (Input.GetMouseButtonDown(0))
                Shoot();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Shoot()
        {
            var direction = GetMousePosition() - shootPosition.position;
            var bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Fire(direction);
        }

        /// <summary>
        /// ����ָ�����
        /// </summary>
        private void AimToMousePosition()
        {
            var direction = GetMousePosition() - shootPosition.position;
            var rotz = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, rotz);
        }

        /// <summary>
        /// ��ȡ�����������λ��
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }

    }
}