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

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
            //ÖØÖÃÉúÃüÖµ
            stats.ResetPlayer();
        }

        public void ResetPlayer()
        {
            stats.ResetPlayer();
            playerAnimations.ResetPlayer();
        }
    }
}