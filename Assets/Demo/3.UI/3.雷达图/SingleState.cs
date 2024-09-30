using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// ����������ֵ
    /// </summary>
    public class SingleState
    {
        private const int MIN = 0;
        private const int MAX = 20;

        //��ǰ��ֵ
        private int stat;

        public SingleState(int stat)
        {
            this.SetStat(stat);
        }

        public void SetStat(int value)
        {
            //ȷ��ֵ�ڷ�Χ��
            stat = Mathf.Clamp(value, MIN, MAX);
        }

        /// <summary>
        /// ��ȡ��ǰֵ��ռ��
        /// </summary>
        /// <returns></returns>
        public float GetStatNormalized()
        {
            return stat / (float)MAX;
        }
    }
}