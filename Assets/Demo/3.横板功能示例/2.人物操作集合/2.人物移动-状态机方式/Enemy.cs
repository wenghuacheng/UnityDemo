using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// 该类也可以作为玩家的基类
    /// </summary>
    public abstract class Enemy : Entity
    {
        [Header("目标设置")]
        [SerializeField] private float targetCheckDistance = 10f;
        [SerializeField] private float attackCoolDown = 2f;
        [SerializeField] private LayerMask whatIsTarget;

        #region 公共属性
        //攻击距离
        public float AttackTargetDistance { get { return attackDistance; } }

        //攻击间距
        public float AttackCoolDown { get { return attackCoolDown; } }
        #endregion

        public override void Attack()
        {
            var targetList = Physics2D.OverlapCircleAll(attackPosition.transform.position, base.attackDistance);
            foreach (var target in targetList)
            {
                //PS:这里用于演示，触发时调用对方的受伤函数
                //target.GetComponent<Player>()?.Damage(this);
            }
        }

        /// <summary>
        /// 目标检测
        /// </summary>
        /// <returns></returns>
        public RaycastHit2D GetTarget()
        {
            return Physics2D.Raycast(this.attackPosition.position, Right, targetCheckDistance, whatIsTarget);
        }

    }
}
