using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// ����Ҳ������Ϊ��ҵĻ���
    /// </summary>
    public abstract class Enemy : Entity
    {
        [Header("Ŀ������")]
        [SerializeField] private float targetCheckDistance = 10f;
        [SerializeField] private float attackCoolDown = 2f;
        [SerializeField] private LayerMask whatIsTarget;

        #region ��������
        //��������
        public float AttackTargetDistance { get { return attackDistance; } }

        //�������
        public float AttackCoolDown { get { return attackCoolDown; } }
        #endregion

        public override void Attack()
        {
            var targetList = Physics2D.OverlapCircleAll(attackPosition.transform.position, base.attackDistance);
            foreach (var target in targetList)
            {
                //PS:����������ʾ������ʱ���öԷ������˺���
                //target.GetComponent<Player>()?.Damage(this);
            }
        }

        /// <summary>
        /// Ŀ����
        /// </summary>
        /// <returns></returns>
        public RaycastHit2D GetTarget()
        {
            return Physics2D.Raycast(this.attackPosition.position, Right, targetCheckDistance, whatIsTarget);
        }

    }
}
