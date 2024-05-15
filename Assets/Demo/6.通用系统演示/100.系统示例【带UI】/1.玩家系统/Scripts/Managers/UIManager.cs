using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.PlayerSysWithUI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private PlayerStats stats;

        [Header("Bars")]
        [SerializeField] private Image healthBar;
        [SerializeField] private Image manaBar;
        [SerializeField] private Image expBar;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI levelTMP;
        [SerializeField] private TextMeshProUGUI healthTMP;
        [SerializeField] private TextMeshProUGUI manaTMP;
        [SerializeField] private TextMeshProUGUI expTMP;

        private void Update()
        {
            UpdatePlayerUI();
        }

        private void UpdatePlayerUI()
        {
            healthBar.fillAmount = stats.Health / stats.MaxHealth;
            manaBar.fillAmount = stats.Mana / stats.MaxMana;
            expBar.fillAmount = stats.CurrentExp / stats.NextLevelExp;

            levelTMP.text = $"Level {stats.Level}";
            healthTMP.text = $"{stats.Health}/{stats.MaxHealth}";
            manaTMP.text = $"{stats.Mana}/{stats.MaxMana}";
            expTMP.text = $"{stats.CurrentExp}/{stats.NextLevelExp}";
        }
    }
}