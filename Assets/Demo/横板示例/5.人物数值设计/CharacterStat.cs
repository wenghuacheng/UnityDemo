using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HB.Demo.Stat
{
    /// <summary>
    /// 人物状态
    /// </summary>
    public class CharacterStat : MonoBehaviour
    {
        //角色受伤
        public event Action<int> OnHurt;
        public event Action<int> OnAttack;
        public event Action<string> OnMessage;

        [SerializeField] private int currentHealth;
        [SerializeField] private MajorStatSO majorStatData;//基础数据
        [SerializeField] private RoleStatSO roleStatData;//角色数据

        //主属性[用于属性汇总显示]
        private MajorStat majorStat;
        //基础属性
        private MajorStat basicStat;
        //角色属性
        private RoleStat roleStat;
        //计算工具类
        private StatCalculator statCalculator;

        protected virtual void Awake()
        {
            majorStat = new MajorStat();

            basicStat = new MajorStat(majorStatData);
            roleStat = new RoleStat(roleStatData);
            statCalculator = new StatCalculator();

            //刷新显示属性
            statCalculator.RefreshMajorStat(majorStat, basicStat, roleStat);
            this.currentHealth = statCalculator.CalculatMaxHealth(basicStat, roleStat);
        }

        protected virtual void Update()
        {
            //判断是否存在异常状态
        }

        /// <summary>
        /// 攻击目标
        /// </summary>
        /// <param name="damage"></param>
        public virtual void DoDamage(CharacterStat stat)
        {
            //闪避判断
            if (statCalculator.CanEvasion(basicStat, roleStat))
            {
                OnMessage?.Invoke("Evasion");
                return;
            }

            var damage = statCalculator.CalculateTotalDamage(majorStat, roleStat, stat.majorStat, stat.roleStat);
            stat.TakeDamage(damage);

            OnAttack?.Invoke(damage);
            OnMessage?.Invoke("attack:" + damage);
        }

        /// <summary>
        /// 魔法攻击
        /// </summary>
        /// <param name="stat"></param>
        public virtual void DoMagicDamage(CharacterStat stat, MagicAttackType type)
        {
        }

        /// <summary>
        /// 收到伤害
        /// </summary>
        /// <param name="attack"></param>
        public virtual void TakeDamage(int damage)
        {
            this.currentHealth -= damage;
            if (this.currentHealth < 0)
            {
                OnMessage?.Invoke("Death");
                Die();
            }

            OnHurt?.Invoke(this.currentHealth);
            OnMessage?.Invoke("health:" + currentHealth);
        }

        /// <summary>
        /// 人物死亡
        /// </summary>
        public virtual void Die()
        {
            //可以由子类重写，也可以通过事件通知
        }


        public void Print()
        {
            Debug.Log("=========");

            Debug.Log($"攻击:{majorStat.damage.GetValue()},魔法攻击:{majorStat.magicDamage.GetValue()},护甲:{majorStat.armor.GetValue()},生命值:{majorStat.health.GetValue()}，魔法值:{majorStat.mana.GetValue()},闪避值:{majorStat.evasion.GetValue()},速度:{majorStat.speed.GetValue()}");
            Debug.Log($"攻击:{basicStat.damage.GetValue()},魔法攻击:{basicStat.magicDamage.GetValue()},护甲:{basicStat.armor.GetValue()},生命值:{basicStat.health.GetValue()}，魔法值:{basicStat.mana.GetValue()},闪避值:{basicStat.evasion.GetValue()},速度:{basicStat.speed.GetValue()}");
            Debug.Log($"力量:{roleStat.strength.GetValue()},敏捷:{roleStat.agility.GetValue()},智力:{roleStat.intelligence.GetValue()},活力:{roleStat.stamina.GetValue()}，幸运:{roleStat.luck.GetValue()},韧性:{roleStat.resilience.GetValue()}");

            Debug.Log("=========");
        }

    }
}