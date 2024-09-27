using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 提供人物升级的属性
    /// </summary>
    public class PlayerUpgrade : MonoBehaviour
    {
        //public static event Action OnPlayerUpgradeEvent;

        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        [Header("Settings")]
        [SerializeField] private UpgradeSettings[] settings;

        private void OnEnable()
        {
            AttributeButton.OnAttributeSelectedEvent += AttributeButton_OnAttributeSelectedEvent;
        }
        private void OnDisable()
        {
            AttributeButton.OnAttributeSelectedEvent -= AttributeButton_OnAttributeSelectedEvent;
        }

        /// <summary>
        /// 属性值发生变更
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AttributeButton_OnAttributeSelectedEvent(AttributeType obj)
        {
            if (stats.AttributePoints == 0) return;
            switch (obj)
            {
                case AttributeType.Strength:
                    UpgradePlayer(0);
                    stats.Strength++;
                    break;
                case AttributeType.Dexterity:
                    UpgradePlayer(1);
                    stats.Dexterity++;
                    break;
                case AttributeType.Intelligence:
                    UpgradePlayer(2);
                    stats.Intelligence++;
                    break;
            }
            stats.AttributePoints--;
        }


        /// <summary>
        /// 玩家升级后使用相关的数值进行提升
        /// </summary>
        private void UpgradePlayer(int upgradeIndex)
        {
            //0：strength，1：Dexterity，2:Intelligence.
            //添加相关属性值后，增加相关的属性

            stats.BaseDamage += settings[upgradeIndex].DamageUpgrade;
            stats.TotalDamage += settings[upgradeIndex].DamageUpgrade;
            stats.MaxHealth += settings[upgradeIndex].HealthUpgrade;
            stats.Health = stats.MaxHealth;
            stats.MaxMana += settings[upgradeIndex].ManaUpgrade;
            stats.Mana = stats.MaxMana;
            stats.CriticalChance += settings[upgradeIndex].CChanceUpgrade;
            stats.CriticalDamage += settings[upgradeIndex].CDamageUpgrade;
        }


    }

    /// <summary>
    /// 人物升级后属性提升
    /// </summary>
    [Serializable]
    public class UpgradeSettings
    {
        public string Name;

        [Header("Values")]
        public float DamageUpgrade;
        public float HealthUpgrade;
        public float ManaUpgrade;
        public float CChanceUpgrade;
        public float CDamageUpgrade;
    }
}