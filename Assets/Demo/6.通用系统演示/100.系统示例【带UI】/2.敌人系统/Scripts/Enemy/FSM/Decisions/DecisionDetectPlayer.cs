using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// Íæ¼Ò¼ì²â
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
        /// Íæ¼Ò¼ì²â
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

        //ÏÔÊ¾¼ì²â·¶Î§
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