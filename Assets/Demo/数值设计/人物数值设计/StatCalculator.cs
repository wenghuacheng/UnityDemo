using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo.Design.Character.Stat
{
    public class StatCalculator
    {
        private const int BASECHANGE = 100;//��������

        #region ����ֵ����
        /// <summary>
        /// ���㹥����
        /// </summary>
        public int CalculateAttack(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.damage.GetValue() + roleStat.strength.GetValue();
        }

        /// <summary>
        /// ���㻤��ֵ
        /// </summary>
        public int CalculateArmor(MajorStat majorStat, RoleStat roleStat)
        {
            var data = majorStat.armor.GetValue() + roleStat.resilience.GetValue() + roleStat.agility.GetValue() * 0.3f;
            return Mathf.RoundToInt(data);
        }

        /// <summary>
        /// ����ħ���˺�
        /// </summary>
        public int CalculateMagicAttack(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.magicDamage.GetValue() + roleStat.intelligence.GetValue();
        }

        /// <summary>
        /// ��������ֵ
        /// </summary>
        public int CalculatEvasion(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.evasion.GetValue() + roleStat.agility.GetValue();
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int CalculatMaxHealth(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.health.GetValue() + roleStat.stamina.GetValue();
        }

        /// <summary>
        /// ����ħ������
        /// </summary>
        public int CalculatMaxMana(MajorStat majorStat, RoleStat roleStat)
        {
            return majorStat.mana.GetValue() + roleStat.intelligence.GetValue();
        }


        /// <summary>
        /// ���㱩���˺�
        /// </summary>
        public int CalculateCritAttack(MajorStat majorStat, RoleStat roleStat)
        {
            //�����˺�Ϊ1.5-3.5
            var damage = CalculateAttack(majorStat, roleStat);
            return CalculateCritAttack(damage);
        }

        /// <summary>
        /// ���㱩���˺�
        /// </summary>
        public int CalculateCritAttack(int damage)
        {
            //�����˺�Ϊ1.5-3.5
            var data = damage * (1 + Random.Range(5, 25) / 10f);
            return Mathf.RoundToInt(data);
        }

        /// <summary>
        /// ���㱩��ֵ
        /// </summary>
        /// <param name="roleStat"></param>
        /// <returns></returns>
        public int CalculateCritChange(RoleStat roleStat)
        {
            return roleStat.luck.GetValue();
        }
        #endregion

        #region ���ʼ���
        /// <summary>
        /// �Ƿ񱩻�
        /// </summary>
        public bool CanCrit(MajorStat majorStat, RoleStat roleStat)
        {
            var data = CalculateCritChange(roleStat);
            return Random.Range(0, data + BASECHANGE) < data;
        }

        /// <summary>
        /// �Ƿ�����
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
        /// �������˺�
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
        /// ˢ����ʾ������
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

