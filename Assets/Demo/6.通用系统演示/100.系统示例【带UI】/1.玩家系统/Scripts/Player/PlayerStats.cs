using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ������������
    /// </summary>
    public enum AttributeType
    {
        Strength,
        Dexterity,
        Intelligence
    }

    /// <summary>
    /// ���״̬��Ϣ
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "��UI��ʾ/���ϵͳ/Player Stats")]
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
        [Range(1f, 100f)] public float ExpMultiplier;//ÿһ�ȼ�������

        [Header("Attack")]
        public float BaseDamage;
        public float CriticalDamage;//�����˺�
        public float CriticalChance;//�����˺�����

        [Header("Attributes")]
        public int Strength;
        public int Dexterity;
        public int Intelligence;
        public int AttributePoints;//�������Ե�

        [HideInInspector] public float TotalExp;
        [HideInInspector] public float TotalDamage;

        public void ResetPlayer()
        {
            Health = MaxHealth;
            Mana = MaxMana;
            Level = 1;
            CurrentExp = 0;
            NextLevelExp = InitialNextLevelExp;
            TotalExp = 0f;
            BaseDamage = 2;
            CriticalChance = 10;
            CriticalDamage = 50;
            Strength = 0;
            Dexterity = 0;
            Intelligence = 0;
            AttributePoints = 0;
        }
    }
}