using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 玩家状态信息
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "带UI演示/玩家系统/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Config")]
        public int Level;

        [Header("Health")]
        public float Health;
        public float MaxHealth;

        [Header("Mana")]
        public float Mana;
        public float MaxMana;

        [Header("XP")]
        public float CurrentExp;
        public float NextLevelExp;
        public float InitialNextLevelExp;
        [Range(1f, 100f)] public float ExpMultiplier;//每一等级的增量

        public void ResetPlayer()
        {
            Health = MaxHealth;
            Mana = MaxMana;
            Level = 1;
            CurrentExp = 0;
            NextLevelExp = InitialNextLevelExp;
        }
    }
}