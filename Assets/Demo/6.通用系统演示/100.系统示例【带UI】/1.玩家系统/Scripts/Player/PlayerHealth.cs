using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ����ֵϵͳ
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
            //����
            if (Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage(2);
            }
        }

        /// <summary>
        /// ����˺�
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
        /// �ָ�������ʰȡ��Ʒ��
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
        /// �Ƿ���Իָ�����
        /// </summary>
        /// <returns></returns>
        public bool CanRestoreHealth()
        {
            return stats.Health > 0 && stats.Health < stats.MaxHealth;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void PlayerDead()
        {
            Debug.Log("����");
            playerAnimations.ShowDeathAnimation();
        }
    }
}