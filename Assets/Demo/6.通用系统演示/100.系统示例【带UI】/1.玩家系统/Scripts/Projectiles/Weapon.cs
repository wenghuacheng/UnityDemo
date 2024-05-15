using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    public enum WeaponType
    {
        Magic, Melee
    }

    /// <summary>
    /// ����
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon_", menuName = "��UI��ʾ/���ϵͳ/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Header("Config")]
        public Sprite Icon;
        public WeaponType WeaponType;
        public float Damage;

        [Header("Projectile")]
        public Projectile ProjectilePrefab;//��ҩԤ����
        public float RequiredMana;//��Ҫ��ħ��ֵ
    }
}