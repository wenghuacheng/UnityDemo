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
        /// 死亡
        /// </summary>
        private void PlayerDead()
        {
            Debug.Log("死亡");
            playerAnimations.ShowDeathAnimation();
        }
    }
}