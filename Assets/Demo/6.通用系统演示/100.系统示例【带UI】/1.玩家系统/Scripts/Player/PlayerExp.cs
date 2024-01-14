using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 玩家经验系统
    /// </summary>
    public class PlayerExp : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerStats stats;

        private void Update()
        {
            //测试
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
            stats.AttributePoints++;//添加可用属性点
            //计算下一级需要的经验
            float curExpRequired = stats.NextLevelExp;
            float newNextLevelExp = Mathf.RoundToInt(curExpRequired + stats.NextLevelExp * stats.ExpMultiplier / 100f);
            stats.NextLevelExp = newNextLevelExp;
        }
    }
}