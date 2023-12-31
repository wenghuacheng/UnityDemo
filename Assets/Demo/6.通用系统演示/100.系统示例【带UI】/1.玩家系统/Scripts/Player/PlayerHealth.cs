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

        public void TakeDamage(float amount)
        {
            stats.Health -= amount;
            if (stats.Health <= 0f)
            {
                PlayerDead();
            }
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