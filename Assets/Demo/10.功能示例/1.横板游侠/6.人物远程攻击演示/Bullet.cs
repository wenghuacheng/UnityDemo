using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Shoot
{
    /// <summary>
    /// ��ҩ�ű�
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private float raycastDistance = 1;

        private float speed = 10f;
        private Vector2 direction;

        private float currentTime = 0f;
        private float aliveTime = 3f;//����ʱ��

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            currentTime += Time.deltaTime;

            RaycastEnemy();
            Movement();
            SelfDestory();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="direction"></param>
        public void Fire(Vector2 direction)
        {
            this.direction = direction;

        }

        /// <summary>
        /// ��ҩ�ƶ�
        /// </summary>
        private void Movement()
        {
            this.transform.Translate(this.direction * speed);


        }

        /// <summary>
        /// ������
        /// </summary>
        private void RaycastEnemy()
        {
            var hit2D = Physics2D.Raycast(this.transform.position, this.transform.up, raycastDistance, whatIsEnemy);
            if (hit2D.transform == null) return;

            Debug.Log("��⵽��");
            Destroy(gameObject);
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SelfDestory()
        {
            if (currentTime >= aliveTime)
                Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            var to = this.transform.position + this.transform.up * raycastDistance;
            Gizmos.DrawLine(this.transform.position, to);
        }
    }
}