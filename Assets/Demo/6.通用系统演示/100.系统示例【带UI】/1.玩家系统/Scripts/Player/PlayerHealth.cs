using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 生命值系统
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        private PlayerAnimations playerAnimations;

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
        }

        private void Update()
        {
            //测试
            if (Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage(2);
            }
        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            if (stats.Health <= 0) return;

            stats.Health -= amount;
            if (stats.Health <= 0f)
            {
                PlayerDead();
            }
        }

        /// <summary>
        /// 恢复生命【拾取物品】
        /// </summary>
        /// <param name="amount"></param>
        public void RestoreHealth(float amount)
        {
            stats.Health += amount;
            if (stats.Health > stats.MaxHealth)
            {
                stats.Health = stats.MaxHealth;
            }
        }

        /// <summary>
        /// 是否可以恢复生命
        /// </summary>
        /// <returns></returns>
        public bool CanRestoreHealth()
        {
            return stats.Health > 0 && stats.Health < stats.MaxHealth;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void PlayerDead()
        {
            Debug.Log("死亡");
            playerAnimations.ShowDeathAnimation();
        }
    }
}