using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ValueDesign.Damage
{
    /// <summary>
    /// �����˺���ʽ
    /// ����������Ļ�����ʽ����һ���ı仯�����������ֵ���ͽ��б��
    /// </summary>
    public class DamageFormula
    {
        /// <summary>
        /// ������ʽ������Ϊ������Ϸ��
        /// ���� - ����
        /// </summary>
        public static float Subtraction(float attack, float defense)
        {
            return MathF.Max(0, attack - defense);
        }

        /// <summary>
        /// �˷���ʽ������ΪMMO��
        /// </summary>
        public static float Multiplication(float attack, float defense)
        {
            var damage = attack * attack / (attack + defense);
            return MathF.Max(0, damage);
        }

        /// <summary>
        /// ������ʽ������ΪMOBA��
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
        /// ������ʽ2
        /// ħ��������ʹ�õĹ�ʽ
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <returns></returns>
        public static float Division2(float attack, float defense, float parameter = 100f)
        {
            var reduceRate = defense / (defense + parameter);//�����ʣ�0-1��
            var damage = attack / (1 - reduceRate);
            return MathF.Max(0, damage);
        }
    }
}
