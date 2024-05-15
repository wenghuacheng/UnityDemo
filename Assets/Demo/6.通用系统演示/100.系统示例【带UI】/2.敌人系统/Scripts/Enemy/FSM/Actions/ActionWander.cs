using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public class ActionWander : FSMAction
    {
        [Header("Config")]
        [SerializeField] private float speed;
        [SerializeField] private float wanderTime;
        [SerializeField] private Vector2 moveRange;//�����ƶ��ķ�Χ

        private Vector3 movePosition;//��һ���ƶ�λ��
        private float timer;

        private void Start()
        {
            GetNewDestination();
        }

        public override void Act()
        {
            timer -= Time.deltaTime;

            //����Ŀ����ƶ�
            Vector3 moveDirection = (movePosition - transform.position).normalized;
            Vector3 movement = moveDirection * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, movePosition) >= 0.5f)
            {
                transform.Translate(movement);
            }

            //�л�Ŀ���
            if (timer <= 0)
            {
                GetNewDestination();
                timer = wanderTime;
            }
        }

        /// <summary>
        /// ��ȡ����ƶ�Ŀ���
        /// </summary>
        private void GetNewDestination()
        {
            float randomX = Random.Range(-moveRange.x, moveRange.x);
            float randomY = Random.Range(-moveRange.y, moveRange.y);

            movePosition = this.transform.position + new Vector3(randomX, randomY);
        }

        private void OnDrawGizmosSelected()
        {
            //����
            if (moveRange != Vector2.zero)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(this.transform.position, moveRange * 2);
                Gizmos.DrawLine(this.transform.position, movePosition);//��ʾ��ǰ�����Ŀ�ĵ�
            }
        }
    }
}