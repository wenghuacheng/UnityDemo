using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 行走状态
    /// </summary>
    public class ActionWander : FSMAction
    {
        [Header("Config")]
        [SerializeField] private float speed;
        [SerializeField] private float wanderTime;
        [SerializeField] private Vector2 moveRange;//可以移动的范围

        private Vector3 movePosition;//下一个移动位置
        private float timer;

        private void Start()
        {
            GetNewDestination();
        }

        public override void Act()
        {
            timer -= Time.deltaTime;

            //向着目标点移动
            Vector3 moveDirection = (movePosition - transform.position).normalized;
            Vector3 movement = moveDirection * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, movePosition) >= 0.5f)
            {
                transform.Translate(movement);
            }

            //切换目标点
            if (timer <= 0)
            {
                GetNewDestination();
                timer = wanderTime;
            }
        }

        /// <summary>
        /// 获取随机移动目标点
        /// </summary>
        private void GetNewDestination()
        {
            float randomX = Random.Range(-moveRange.x, moveRange.x);
            float randomY = Random.Range(-moveRange.y, moveRange.y);

            movePosition = this.transform.position + new Vector3(randomX, randomY);
        }

        private void OnDrawGizmosSelected()
        {
            //调试
            if (moveRange != Vector2.zero)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(this.transform.position, moveRange * 2);
                Gizmos.DrawLine(this.transform.position, movePosition);//显示当前物体的目的地
            }
        }
    }
}