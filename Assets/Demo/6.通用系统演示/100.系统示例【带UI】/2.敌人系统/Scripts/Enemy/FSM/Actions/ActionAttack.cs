using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 攻击行为
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
                //给玩家造成伤害
                IDamageable player = enemyBrain.Player.GetComponent<IDamageable>();
                player.TakeDamage(damage);
                //PS:可以写在TakeDamage方法中，这里为了切割就在这边显示
                DamageManager.Instance.ShowDamageText(damage, enemyBrain.Player);

                timer = damageCoolDown;
            }
        }
    }
}