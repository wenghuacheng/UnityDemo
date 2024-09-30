using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    public class TankTowerRotateTowardsDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 2;

        private bool IsRotating;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var position = Input.mousePosition;
            var mousePos = mainCamera.ScreenToWorldPoint(position);

            if (!IsRotating)
            {
                StartCoroutine(RotateByLookRotation(mousePos));
            }
        }

        /// <summary>
        /// ͨ��RotateTowards��ת
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IEnumerator RotateByLookRotation(Vector3 target)
        {
            IsRotating = true;

            Vector2 direction = (target - transform.position).normalized;

            //ͨ�������ȡ��Ԫ��
            Quaternion targetRotation = Quaternion.LookRotation(this.transform.up, direction);

            double angle = Quaternion.Angle(transform.rotation, targetRotation);
            while (angle > 1f)
            {
                //һ�����ת��Ŀ��Ƕ�
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

                //�������ǰ�ļн�
                angle = Quaternion.Angle(transform.rotation, targetRotation);
                yield return null;
            }

            IsRotating = false;
        }
    }
}