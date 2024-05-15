using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ValueDesign.Damage
{
    /// <summary>
    /// 基础伤害公式
    /// 可以在下面的基础公式进行一定的变化，将参与的数值类型进行变更
    /// </summary>
    public class DamageFormula
    {
        /// <summary>
        /// 减法公式【主流为单机游戏】
        /// 攻击 - 防御
        /// </summary>
        public static float Subtraction(float attack, float defense)
        {
            return MathF.Max(0, attack - defense);
        }

        /// <summary>
        /// 乘法公式【主流为MMO】
        /// </summary>
        public static float Multiplication(float attack, float defense)
        {
            var damage = attack * attack / (attack + defense);
            return MathF.Max(0, damage);
        }

        /// <summary>
        /// 除法公式【主流为MOBA】
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <returns></returns>
        public static float Division(float attack, float defense, float parameter = 100f)
        {
            var damage = attack / (1 + (defense / parameter));
            return MathF.Max(0, damage);
        }


        /// <summary>
        /// 除法公式2
        /// 魔兽世界中使用的公式
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <returns></returns>
        public static float Division2(float attack, float defense, float parameter = 100f)
        {
            var reduceRate = defense / (defense + parameter);//免伤率（0-1）
            var damage = attack / (1 - reduceRate);
            return MathF.Max(0, damage);
        }
    }
}
