using HB.Demo.Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    public class MagicStat
    {
        [Header("魔法属性")]
        //火焰伤害
        public SingleStat fireDamage;
        //冰冻伤害
        public SingleStat iceDamage;
        //闪电伤害
        public SingleStat lightDamage;
        //魔法值
        public SingleStat mana;

        [Header("负面状态")]
        //是否被灼烧
        //该状态下持续收到伤害
        public bool isIgnited;
        //是否被冰冻
        //当状态下移动减速
        public bool isChilled;
        //是否被麻痹
        //该状态下攻击miss
        public bool isShocked;

        /// <summary>
        /// 获取攻击数值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetMagicAttackValue(MagicAttackType type)
        {
            switch (type)
            {
                case MagicAttackType.fire:
                    return fireDamage.GetValue();
                case MagicAttackType.ice:
                    return iceDamage.GetValue();
                case MagicAttackType.light:
                    return lightDamage.GetValue();
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 是否获取负面状态
        /// </summary>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        public void ApplyDebuffState(MagicAttackType type, int damage)
        {
            //已经存在负面状态则判断
            if (isIgnited || isChilled || isShocked) return;

            //判断是否有几率获取负面特性
            if (Random.Range(0, damage * 3) <= damage)
            {
                switch (type)
                {
                    case MagicAttackType.fire:
                        isIgnited = true;
                        break;
                    case MagicAttackType.ice:
                        isChilled = true;
                        break;
                    case MagicAttackType.light:
                        isShocked = true;
                        break;
                }
            }
        }
    }
}