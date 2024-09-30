using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// 单个能力数值
    /// </summary>
    public class SingleState
    {
        private const int MIN = 0;
        private const int MAX = 20;

        //当前数值
        private int stat;

        public SingleState(int stat)
        {
            this.SetStat(stat);
        }

        public void SetStat(int value)
        {
            //确保值在范围内
            stat = Mathf.Clamp(value, MIN, MAX);
        }

        /// <summary>
        /// 获取当前值的占比
        /// </summary>
        /// <returns></returns>
        public float GetStatNormalized()
        {
            return stat / (float)MAX;
        }
    }
}