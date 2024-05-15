using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ������Ϊ
    /// </summary>
    public class ActionAttack : FSMAction
    {
        [Header("Config")]
        [SerializeField] private float damage;
        [SerializeField] private float damageCoolDown;

        private EnemyBrain enemyBrain;
        private float timer;

        private void Awake()
        {
            enemyBrain = GetComponent<EnemyBrain>();
        }

        public override void Act()
        {
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            if (enemyBrain.Player == null) return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //���������˺�
                IDamageable player = enemyBrain.Player.GetComponent<IDamageable>();
                player.TakeDamage(damage);
                //PS:����д��TakeDamage�����У�����Ϊ���и���������ʾ
                DamageManager.Instance.ShowDamageText(damage, enemyBrain.Player);

                timer = damageCoolDown;
            }
        }
    }
}