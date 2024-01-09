using Demo.Common.EnemySysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ×·»÷ÐÐÎª
    /// </summary>
    public class ActionChase : FSMAction
    {
        [Header("Config")]
        [SerializeField] private float chaseSpeed;

        private EnemyBrain enemyBrain;

        private void Awake()
        {
            enemyBrain = GetComponent<EnemyBrain>();
        }

        public override void Act()
        {
            ChasePlayer();
        }

        private void ChasePlayer()
        {
            if (enemyBrain == null) return;

            var dirToPlayer = enemyBrain.Player.transform.position - enemyBrain.transform.position;
            if (dirToPlayer.magnitude >= 1.3f)
            {
                enemyBrain.transform.Translate(dirToPlayer.normalized * chaseSpeed * Time.deltaTime);
            }

        }
    }
}