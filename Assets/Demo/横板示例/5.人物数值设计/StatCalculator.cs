using HB.Demo.Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HB.Demo.Stat
{
    public class StatCalculator
    {
        private const int BASECHANGE = 100;//基础概率

        #region 常规值计算
        /// <summary>
        /// 计算攻击力
        /// </summary>
        public int CalculateAttack(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.damage.GetValue() + roleStat.strength.GetValue();
        }

        /// <summary>
        /// 计算护甲值
        /// </summary>
        public int CalculateArmor(MajorStat majorStat, RoleStat roleStat)
        {
            var data = majorStat.armor.GetValue() + roleStat.resilience.GetValue() + roleStat.agility.GetValue() * 0.3f;
            return Mathf.RoundToInt(data);
        }

        /// <summary>
        /// 计算魔法伤害
        /// </summary>
        public int CalculateMagicAttack(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.magicDamage.GetValue() + roleStat.intelligence.GetValue();
        }

        /// <summary>
        /// 计算闪避值
        /// </summary>
        public int CalculatEvasion(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.evasion.GetValue() + roleStat.agility.GetValue();
        }

        /// <summary>
        /// 计算生命上限
        /// </summary>
        public int CalculatMaxHealth(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.health.GetValue() + roleStat.stamina.GetValue();
        }

        /// <summary>
        /// 计算魔法上限
        /// </summary>
        public int CalculatMaxMana(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.mana.GetValue() + roleStat.intelligence.GetValue();
        }


        /// <summary>
        /// 计算暴击伤害
        /// </summary>
        public int CalculateCritAttack(MajorStat majorStat, RoleStat roleStat)
        {
            //暴击伤害为1.5-3.5
            var damage = CalculateAttack(majorStat, roleStat);
            return CalculateCritAttack(damage);
        }

        /// <summary>
        /// 计算暴击伤害
        /// </summary>
        public int CalculateCritAttack(int damage)
        {
            //暴击伤害为1.5-3.5
            var data = damage * (1 + Random.Range(5, 25) / 10f);
            return Mathf.RoundToInt(data);
        }

        /// <summary>
        /// 计算暴击值
        /// </summary>
        /// <param name="roleStat"></param>
        /// <returns></returns>
        public int CalculateCritChange(RoleStat roleStat)
        {
            return roleStat.luck.GetValue();
        }
        #endregion

        #region 概率计算
        /// <summary>
        /// 是否暴击
        /// </summary>
        public bool CanCrit(MajorStat majorStat, RoleStat roleStat)
        {
            var data = CalculateCritChange(roleStat);
            return Random.Range(0, data + BASECHANGE) < data;
        }

        /// <summary>
        /// 是否闪避
        /// </summary>
        /// <param name="majorStat"></param>
        /// <param name="roleStat"></param>
        /// <returns></returns>
        public bool CanEvasion(MajorStat majorStat, RoleStat roleStat)
        {
            var data = CalculatEvasion(majorStat, roleStat);
            return Random.Range(0, data + BASECHANGE) < data;
        }
        #endregion

        /// <summary>
        /// 计算总伤害
        /// </summary>
        public int CalculateTotalDamage(MajorStat majorStat, RoleStat roleStat, MajorStat targetMajorStat, RoleStat targetRoleStat)
        {
            var damage = CalculateAttack(majorStat, roleStat);
            if (CanCrit(majorStat, roleStat))
                damage = CalculateCritAttack(majorStat, roleStat);

            var armor = CalculateArmor(targetMajorStat, roleStat);
            damage -= armor;
            damage = Mathf.Max(damage, 0);
            return damage;
        }


        /// <summary>
        /// 刷新显示主属性
        /// </summary>
        public void RefreshMajorStat(MajorStat majorStat, MajorStat basicStat, RoleStat roleStat)
        {
            majorStat.armor.SetDefaultValue(CalculateArmor(basicStat, roleStat));
            majorStat.damage.SetDefaultValue(CalculateAttack(basicStat, roleStat));
            majorStat.evasion.SetDefaultValue(CalculatEvasion(basicStat, roleStat));
            majorStat.health.SetDefaultValue(CalculatMaxHealth(basicStat, roleStat));
            majorStat.magicDamage.SetDefaultValue(CalculateMagicAttack(basicStat, roleStat));
            majorStat.mana.SetDefaultValue(CalculatMaxMana(basicStat, roleStat));
            majorStat.speed.SetDefaultValue(majorStat.speed.GetValue());
        }
    }
}

