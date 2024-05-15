using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Demo.UI
{
    public class HealthSystem : MonoBehaviour
    {
        //伤害与回血事件
        public event Action OnDamaged;
        public event Action OnHealthed;

        //当前血量
        private int healthAmount;
        //最大血量值
        private int healthAmountMax;

        public HealthSystem(int healthAmount)
        {
            //初始化时为满血
            this.healthAmount = healthAmount;
            this.healthAmountMax = healthAmount;
        }

        /// <summary>
        /// 受伤
        /// </summary>
        public void Damage(int amount)
        {
            healthAmount -= amount;
            healthAmount = Math.Max(0, healthAmount);
            OnDamaged?.Invoke();
        }

        /// <summary>
        /// 回血
        /// </summary>
        public void Heal(int amount)
        {
            healthAmount += amount;
            healthAmount = Math.Min(healthAmountMax, healthAmount);
            OnHealthed?.Invoke();
        }

        /// <summary>
        /// 血量占比 
        /// </summary>
        /// <returns></returns>
        public float GetHealthNormalized()
        {
            return (float)healthAmount / healthAmountMax;
        }
    }
}