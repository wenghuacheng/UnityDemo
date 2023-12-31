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

        private void Update()
        {
            //����
            if (Input.GetKeyDown(KeyCode.M))
            {
                UseMana(2);
            }
        }

        public void UseMana(float amount)
        {
            if (stats.Mana >= amount)
            {
                stats.Mana = Mathf.Max(stats.Mana -= amount, 0f);
            }
        }
    }
}