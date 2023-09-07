using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HB.Demo.Stat
{
    /// <summary>
    /// 单个数值对象
    /// </summary>
    [Serializable]
    public class SingleStat
    {
        [SerializeField] private int baseValue;

        private List<SingleStat> modifiers = new List<SingleStat>();

        public SingleStat()
        {

        }

        public SingleStat(int defaultValue)
        {
            SetDefaultValue(defaultValue);
        }

        public int GetValue()
        {
            int result = baseValue;
           
            return result;
        }

        /// <summary>
        /// 设置基础数值
        /// </summary>
        /// <param name="defaultValue"></param>
        public void SetDefaultValue(int defaultValue)
        {
            baseValue = defaultValue;
        }
    }
}
