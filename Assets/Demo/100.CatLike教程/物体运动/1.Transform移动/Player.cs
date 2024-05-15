using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.Movement.Chaper01
{
    public class Player : MonoBehaviour
    {
        [SerializeField, Range(0, 100f)] private float maxSpeed = 3;
        [SerializeField, Range(0, 100f)] private float maxAcceleration = 1f;
        [SerializeField] private Rect allowedArea = new Rect(-5, -5, 10, 10);

        void Start()
        {

        }

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// �˶�
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            var velocity = new Vector3(x, 0, y);

            #region ���ٶ�
            //���ٶȡ�ͨ��һ�����ٶȱ������䲻��ֱ�ӵ�������ٶȡ�
            Vector3 desiredVelocity = velocity * maxSpeed;
            float maxSpeedChanged = maxAcceleration * Time.deltaTime;

            if (velocity.x < desiredVelocity.x)
                velocity.x = Mathf.Min(velocity.x + maxSpeedChanged, desiredVelocity.x);
            else if (velocity.x > desiredVelocity.x)
                velocity.x = Mathf.Max(velocity.x - maxSpeedChanged, desiredVelocity.x);
            #endregion

            var displacement = velocity * Time.deltaTime * maxSpeed;
            Vector3 newPosition = transform.localPosition + displacement;

            #region �˶����Ʒ�Χ
            //�˶����Ʒ�Χ
            if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
            {
                newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
                newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
            }
            #endregion

            transform.localPosition = newPosition;
        }
    }
}