using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    /// <summary>
    /// 单个数值对象
    /// </summary>
    [Serializable]
    public class SingleStat
    {
        [SerializeField] private int baseValue;

        //影响效果
        private List<EffectValueSO> modifiers = new List<EffectValueSO>();
        //Key:id,value:count
        private Dictionary<string, int> modifierDict = new Dictionary<string, int>();

        public SingleStat()
        {

        }

        public SingleStat(int defaultValue)
        {
            SetDefaultValue(defaultValue);
        }

        public int GetValue()
        {
            int result = baseValue + CalculateEffectValue();
            return result;
        }

        /// <summary>
        /// 添加附加效果
        /// </summary>
        /// <param name="effectValue"></param>
        public void AddEffect(EffectValueSO effectValue)
        {
            if (!modifierDict.ContainsKey(effectValue.id))
            {
                modifiers.Add(effectValue);
                modifierDict.Add(effectValue.id, 1);
                return;
            }

            //检查叠加的状态数量是否超过的最大数量
            int count = modifierDict[effectValue.id];
            if (effectValue.maxCount <= count)
                return;

            modifierDict[effectValue.id] = count + 1;
        }

        /// <summary>
        /// 移除效果
        /// </summary>
        /// <param name="effectValue"></param>
        public void RemoveEffect(EffectValueSO effectValue)
        {
            if (!modifierDict.ContainsKey(effectValue.id))
                return;

            int count = modifierDict[effectValue.id];
            if (count <= 1)
            {
                modifiers.RemoveAll(p => p.id == effectValue.id);
                modifierDict.Remove(effectValue.id);
            }
            else
            {
                modifierDict[effectValue.id] = count - 1;
            }
        }

        /// <summary>
        /// 设置基础数值
        /// </summary>
        /// <param name="defaultValue"></param>
        public void SetDefaultValue(int defaultValue)
        {
            baseValue = defaultValue;
        }

        /// <summary>
        /// 计算附加数值
        /// </summary>
        private int CalculateEffectValue()
        {
            int result = 0;
            foreach (var pair in modifierDict)
            {
                var count = modifierDict[pair.Key];
                var modifier = modifiers.FirstOrDefault(p => p.id == pair.Key);
                if (modifier == null) continue;

                if (modifier.effectType == EffectValueSO.EffectTypeEnum.Buff)
                    result += modifier.data * count;
                else
                    result -= modifier.data * count;
            }
            return result;
        }
    }
}
