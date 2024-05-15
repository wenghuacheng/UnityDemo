using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ��Ҿ���ϵͳ
    /// </summary>
    public class PlayerExp : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        private void Update()
        {
            //����
            if (Input.GetKeyDown(KeyCode.X))
            {
                AddExp(100);
            }
        }

        public void AddExp(float amount)
        {
            stats.TotalExp += amount;
            stats.CurrentExp += amount;
            while (stats.CurrentExp >= stats.NextLevelExp)
            {
                stats.CurrentExp -= stats.NextLevelExp;
                NextLevel();
            }
        }

        private void NextLevel()
        {
            stats.Level++;
            stats.AttributePoints++;//��ӿ������Ե�
            //������һ����Ҫ�ľ���
            float curExpRequired = stats.NextLevelExp;
            float newNextLevelExp = Mathf.RoundToInt(curExpRequired + stats.NextLevelExp * stats.ExpMultiplier / 100f);
            stats.NextLevelExp = newNextLevelExp;
        }
    }
}