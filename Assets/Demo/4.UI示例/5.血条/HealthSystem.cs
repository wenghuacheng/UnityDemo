using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Demo.UI
{
    public class HealthSystem : MonoBehaviour
    {
        //�˺����Ѫ�¼�
        public event Action OnDamaged;
        public event Action OnHealthed;

        //��ǰѪ��
        private int healthAmount;
        //���Ѫ��ֵ
        private int healthAmountMax;

        public HealthSystem(int healthAmount)
        {
            //��ʼ��ʱΪ��Ѫ
            this.healthAmount = healthAmount;
            this.healthAmountMax = healthAmount;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Damage(int amount)
        {
            healthAmount -= amount;
            healthAmount = Math.Max(0, healthAmount);
            OnDamaged?.Invoke();
        }

        /// <summary>
        /// ��Ѫ
        /// </summary>
        public void Heal(int amount)
        {
            healthAmount += amount;
            healthAmount = Math.Min(healthAmountMax, healthAmount);
            OnHealthed?.Invoke();
        }

        /// <summary>
        /// Ѫ��ռ�� 
        /// </summary>
        /// <returns></returns>
        public float GetHealthNormalized()
        {
            return (float)healthAmount / healthAmountMax;
        }
    }
}