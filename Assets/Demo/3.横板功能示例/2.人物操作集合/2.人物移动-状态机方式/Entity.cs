using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// 玩家/敌人基类
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected EntityVisual entityVisual;

        [Header("地面检测")]
        [SerializeField] protected Transform groundCheckTransform;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float groundCheckDistance = 0.1f;

        [Header("墙体检测")]
        [SerializeField] protected Transform wallCheckTransform;
        [SerializeField] protected float wallCheckDistance = 0.1f;

        [Header("攻击范围")]
        [SerializeField] protected Transform attackPosition;
        [SerializeField] protected float attackDistance = 2f;
        [SerializeField] protected float strikeFlyDuration = 0.5f;//击飞时间
        [SerializeField] protected Vector2 strikeFlyStrength;//击飞力度【水平和垂直】

        //是否面向右边
        protected bool isFaceRight = true;

        #region 公共属性
        public Animator Animator { get { return animator; } }

        public Rigidbody2D Rb { get { return rb; } }

        //人物正面方向
        public Vector3 Right
        {
            get
            {
                //以人物的右方向作为正方向，旋转后需要转换其为世界坐标系
                return transform.TransformDirection(Vector3.right);
            }
        }
        #endregion

        /// <summary>
        /// 地面检测
        /// </summary>
        /// <returns></returns>
        public bool IsOnGround()
        {
            var hit2D = Physics2D.Raycast(this.groundCheckTransform.position, Vector2.down, groundCheckDistance, whatIsGround);
            return hit2D.collider != null;
        }

        /// <summary>
        /// 墙体检测
        /// </summary>
        /// <returns></returns>
        public bool IsWallDetected()
        {
            //将人物的右方向进行修改
            var hit2D = Physics2D.Raycast(this.wallCheckTransform.position, Right, wallCheckDistance, whatIsGround);
            return hit2D.collider != null;
        }

        #region 人物翻转
        /// <summary>
        /// 翻转控制
        /// </summary>
        protected void FlipController()
        {
            if (rb.velocity.x > 0 && !isFaceRight)
            {
                Flip();
                isFaceRight = true;
            }
            else if (rb.velocity.x < 0 && isFaceRight)
            {
                Flip();
                isFaceRight = false;
            }

        }

        /// <summary>
        /// 翻转
        /// </summary>
        public void Flip()
        {
            this.transform.Rotate(0, 180, 0);
        }
        #endregion

        #region 攻击&被命中
        /// <summary>
        /// 攻击
        /// </summary>
        public abstract void Attack();


        /// <summary>
        /// 被命中
        /// </summary>
        public void Damage(Entity entity)
        {
            entityVisual.FlashAsync();
            StartCoroutine(StrikeFly(entity.Right));
        }

        /// <summary>
        /// 被击飞
        /// </summary>
        protected IEnumerator StrikeFly(Vector2 direction)
        {
            this.rb.velocity = new Vector2(direction.x * strikeFlyStrength.x, strikeFlyStrength.y);
            yield return new WaitForSeconds(strikeFlyDuration);
        }
        #endregion


        #region 测试
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.groundCheckTransform.position, this.groundCheckTransform.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(this.wallCheckTransform.position, this.wallCheckTransform.position + Vector3.right * wallCheckDistance);

            Gizmos.DrawWireSphere(attackPosition.transform.position, attackDistance);
        }
        #endregion
    }
}