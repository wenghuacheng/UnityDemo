using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ��Ҽ��
    /// </summary>
    public class DecisionDetectPlayer : FSMDecision
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
            return DetectPlayer();
        }

        /// <summary>
        /// ��Ҽ��
        /// </summary>
        /// <returns></returns>
        private bool DetectPlayer()
        {
            var playerCollider = Physics2D.OverlapCircle(enmeyBrain.transform.position, range, playerMask);
            if (playerCollider != null)
            {
                enmeyBrain.Player = playerCollider.transform;
                return true;
            }
            enmeyBrain.Player = null;
            return false;
        }

        //��ʾ��ⷶΧ
        private void OnDrawGizmos()
        {
            if (enmeyBrain != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(enmeyBrain.transform.position, range);
            }
        }
    }
}