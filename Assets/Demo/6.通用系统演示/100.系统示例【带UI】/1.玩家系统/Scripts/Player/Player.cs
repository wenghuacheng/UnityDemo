using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    public class Player : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        public PlayerStats Stats => stats;

        private PlayerAnimations playerAnimations;
        private PlayerMana playerMana;
        private PlayerHealth playerHealth;

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
            playerMana = GetComponent<PlayerMana>();
            playerHealth = GetComponent<PlayerHealth>();
            //ÖØÖÃÉúÃüÖµ
            stats.ResetPlayer();
        }

        public void ResetPlayer()
        {
            stats.ResetPlayer();
            playerAnimations.ResetPlayer();
            playerMana.Reset();
        }
    }
}