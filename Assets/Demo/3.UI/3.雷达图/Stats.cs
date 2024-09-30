using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Stats
    {
        public event Action OnStatsChanged;

        public enum StatType
        {
            Attack,
            Defence,
            Speed,
            Mana,
            Health
        }

        //单个数值对象
        private SingleState attackStat;
        private SingleState defenceStat;
        private SingleState speedStat;
        private SingleState manaStat;
        private SingleState healthStat;

        public Stats(int attack, int defence, int speed, int mana, int health)
        {
            attackStat = new SingleState(attack);
            defenceStat = new SingleState(defence);
            speedStat = new SingleState(speed);
            manaStat = new SingleState(mana);
            healthStat = new SingleState(health);
        }

        /// <summary>
        /// 根据类型获取数值对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SingleState GetSingleState(StatType type)
        {
            switch (type)
            {
                default:
                case StatType.Attack:
                    return attackStat;
                case StatType.Defence:
                    return defenceStat;
                case StatType.Speed:
                    return speedStat;
                case StatType.Mana:
                    return manaStat;
                case StatType.Health:
                    return healthStat;
            }
        }

        /// <summary>
        /// 获取数值的占比值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public float GetStateNormalized(StatType type)
        {
            return GetSingleState(type).GetStatNormalized();
        }

        /// <summary>
        /// 设置数值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stat"></param>
        public void SetStat(StatType type, int stat)
        {
            var singleStat = GetSingleState(type);
            singleStat.SetStat(stat);
            OnStatsChanged?.Invoke();
        }
    }
}