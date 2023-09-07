using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Stat/Effect", order = 2)]
    public class EffectValueSO : ScriptableObject
    {
        public enum EffectTypeEnum
        {
            Buff, Debuff
        }

        public string id;

        //效果类型
        public EffectTypeEnum effectType;

        //值
        public int data;

        //描述
        public string description;
    }
}