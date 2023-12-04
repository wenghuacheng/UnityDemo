using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.HideRaycast
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private float RotationSpeed = 30;
        private float distance = 20;

        private Transform lockPlayer;
        private float lockRemainTime;
        private float maxLockTime = 1f;

        void Start()
        {
            //如果查询的起始点在碰撞器内部，那么该碰撞器将不会被返回作为查询结果
            Physics2D.queriesStartInColliders = false;
        }

        void Update()
        {
            AutoRotation();
            CheckRay();
        }

        /// <summary>
        /// 自动旋转
        /// </summary>
        private void AutoRotation()
        {
            if (lockPlayer == null)
                this.transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
            else
            {
                //看向玩家
                var vectorToTarget = (lockPlayer.transform.position - this.transform.position).normalized;
                this.transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }
        }

        /// <summary>
        /// 射线检测
        /// </summary>
        private void CheckRay()
        {
            lockRemainTime -= Time.deltaTime;
            if (lockPlayer != null && lockRemainTime <= 0)
            {
                lockPlayer = null;
            }

            var hit2D = Physics2D.Raycast(this.transform.position, this.transform.up, distance);
            if (hit2D.collider != null)
            {
                SetLine(hit2D.point);
                if (hit2D.collider.tag == "Player")
                {
                    //锁定玩家
                    lockPlayer = hit2D.transform;
                    lockRemainTime = maxLockTime;
                    Debug.Log("玩家被盯上了");
                }
            }
            else
            {
                Vector2 target = Vector2.zero;
                if (lockPlayer != null && lockRemainTime > 0)
                {
                    //已经锁定了目标
                    target = lockPlayer.transform.position;
                }
                else
                {
                    //未搜索到目标
                    target = this.transform.position + this.transform.up * distance;
                }

                SetLine(target);
            }
        }

        /// <summary>
        /// 绘制视野线
        /// </summary>
        private void SetLine(Vector2 target)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, target);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(this.transform.position, this.transform.up * distance);
        }
    }
}