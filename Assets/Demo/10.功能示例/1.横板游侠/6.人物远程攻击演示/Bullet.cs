using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Shoot
{
    /// <summary>
    /// 弹药脚本
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private float raycastDistance = 1;

        private float speed = 10f;
        private Vector2 direction;

        private float currentTime = 0f;
        private float aliveTime = 3f;//生存时间

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
        /// 发射
        /// </summary>
        /// <param name="direction"></param>
        public void Fire(Vector2 direction)
        {
            this.direction = direction;

        }

        /// <summary>
        /// 弹药移动
        /// </summary>
        private void Movement()
        {
            this.transform.Translate(this.direction * speed);


        }

        /// <summary>
        /// 检测敌人
        /// </summary>
        private void RaycastEnemy()
        {
            var hit2D = Physics2D.Raycast(this.transform.position, this.transform.up, raycastDistance, whatIsEnemy);
            if (hit2D.transform == null) return;

            Debug.Log("检测到了");
            Destroy(gameObject);
        }

        /// <summary>
        /// 自我销毁
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