using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ħ��ϵͳ
    /// </summary>
    public class PlayerMana : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        public float CurrentMana { get; private set; }

        private void Awake()
        {
            CurrentMana = stats.Mana;
        }

        private void Update()
        {
            //����
            if (Input.GetKeyDown(KeyCode.M))
            {
                UseMana(2);
            }
        }

        /// <summary>
        /// ħ������
        /// </summary>
        /// <param name="amount"></param>
        public void UseMana(float amount)
        {
            if (CurrentMana >= amount)
            {
                CurrentMana = Mathf.Max(stats.Mana -= amount, 0f);
            }
        }

        /// <summary>
        /// �ָ�ħ����ʰȡ��Ʒ��
        /// </summary>
        /// <param name="amount"></param>
        public void RestoreMana(float amount)
        {
            stats.Mana += amount;
            if (stats.Mana > stats.MaxMana)
            {
                stats.Mana = stats.MaxMana;
            }
        }

        /// <summary>
        /// �Ƿ���Իָ�ħ��
        /// </summary>
        /// <returns></returns>
        public bool CanRestoreMana()
        {
            return stats.Mana > 0 && stats.Mana < stats.MaxMana;
        }

        public void Reset()
        {
            CurrentMana = stats.MaxMana;
        }
    }
}