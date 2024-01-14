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
    /// 武器
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon_", menuName = "带UI演示/玩家系统/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Header("Config")]
        public Sprite Icon;
        public WeaponType WeaponType;
        public float Damage;

        [Header("Projectile")]
        public Projectile ProjectilePrefab;//弹药预制体
        public float RequiredMana;//需要的魔法值
    }
}