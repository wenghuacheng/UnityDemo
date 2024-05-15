using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI.StatPanel
{
    public class StatUIManager : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private PlayerStats stats;


        [Header("Stats Panel")]
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private TextMeshProUGUI statLevelTMP;
        [SerializeField] private TextMeshProUGUI statDamageTMP;
        [SerializeField] private TextMeshProUGUI statCChangeTMP;
        [SerializeField] private TextMeshProUGUI statCDamageTMP;
        [SerializeField] private TextMeshProUGUI statTotalExpTMP;
        [SerializeField] private TextMeshProUGUI statCurrentExpTMP;
        [SerializeField] private TextMeshProUGUI statRequiredExpTMP;

        [SerializeField] private TextMeshProUGUI attributePointTMP;
        [SerializeField] private TextMeshProUGUI strengthTMP;
        [SerializeField] private TextMeshProUGUI dexterityTMP;
        [SerializeField] private TextMeshProUGUI intelligenceTMP;

        private void OnEnable()
        {
            AttributeButton.OnAttributeSelectedEvent += AttributeButton_OnAttributeSelectedEvent;
        }

        private void OnDisable()
        {
            AttributeButton.OnAttributeSelectedEvent -= AttributeButton_OnAttributeSelectedEvent;
        }

        private void AttributeButton_OnAttributeSelectedEvent(AttributeType obj)
        {
            UpdateStatsPanel();
        }

        /// <summary>
        /// 打开/关闭面板
        /// </summary>
        public void OpenClosePanel()
        {
            statsPanel.SetActive(!statsPanel.activeSelf);
            if (statsPanel.activeSelf)
            {
                //开始面板时才更新数据
                UpdateStatsPanel();
            }
        }


        private void UpdateStatsPanel()
        {
            statLevelTMP.text = stats.Level.ToString();
            statDamageTMP.text = stats.TotalDamage.ToString();
            statCChangeTMP.text = stats.CriticalChance.ToString();
            statCDamageTMP.text = stats.CriticalDamage.ToString();
            statTotalExpTMP.text = stats.TotalExp.ToString();
            statCurrentExpTMP.text = stats.CurrentExp.ToString();
            statRequiredExpTMP.text = stats.NextLevelExp.ToString();

            attributePointTMP.text = $"Points {stats.AttributePoints}";
            strengthTMP.text = stats.Strength.ToString();
            dexterityTMP.text = stats.Dexterity.ToString();
            intelligenceTMP.text = stats.Intelligence.ToString();
        }
    }
}