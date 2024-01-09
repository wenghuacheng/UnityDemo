using Demo.Common.EnemySysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    public class DecisionAttackRange : FSMDecision
    {
        [Header("Config")]
        [SerializeField] private float range;
        [SerializeField] private LayerMask playerMask;

        private EnemyBrain enmeyBrain;

        private void Awake()
        {
            enmeyBrain = GetComponent<EnemyBrain>();
        }

        public override bool Decide()
        {
            return PlayerInAttackRange();
        }

        /// <summary>
        /// ¼ì²âÍæ¼ÒÊÇ·ñÔÚ¹¥»÷·¶Î§ÄÚ
        /// </summary>
        /// <returns></returns>
        private bool PlayerInAttackRange()
        {
            if (enmeyBrain.Player == null) return false;

            var playerCollider = Physics2D.OverlapCircle(enmeyBrain.transform.position, range, playerMask);
            if (playerCollider != null)
            {
                return true;
            }
            return false;
        }

        //ÏÔÊ¾¼ì²â·¶Î§
        private void OnDrawGizmos()
        {
            if (enmeyBrain != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(enmeyBrain.transform.position, range);
            }
        }
    }
}