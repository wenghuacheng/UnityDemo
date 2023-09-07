using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    /// <summary>
    /// 基础属性
    /// </summary>
    [CreateAssetMenu(fileName = "Major", menuName = "Stat/Major", order = 1)]
    public class MajorStatSO : ScriptableObject
    {
        //攻击
        public int damage;
        //魔法攻击
        public int magicDamage;
        //生命上限
        public int health;
        //移速
        public int speed;
        //护甲值
        public int armor;
        //闪避值
        public int evasion;
        //魔法上限
        public int mana;
    }
}
