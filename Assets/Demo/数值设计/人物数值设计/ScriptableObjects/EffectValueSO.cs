using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    [CreateAssetMenu(fileName = "Effect_", menuName = "人物数值/影响效果", order = 2)]
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

        //最大叠加数量
        public int maxCount = 1;
    }
}