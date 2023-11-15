using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    /// <summary>
    /// 主属性
    /// </summary>
    public class MajorStat
    {
        public MajorStat()
        {
            InitData();
        }

        public MajorStat(MajorStatSO so)
        {
            InitData(so);
        }

        //攻击
        public SingleStat damage;
        //魔法攻击
        public SingleStat magicDamage;
        //生命上限
        public SingleStat health;
        //移速
        public SingleStat speed;
        //护甲值
        public SingleStat armor;
        //闪避值
        public SingleStat evasion;
        //魔法上限
        public SingleStat mana;


        private void InitData()
        {
            damage = new SingleStat();
            magicDamage = new SingleStat();
            health = new SingleStat();
            speed = new SingleStat();
            armor = new SingleStat();
            evasion = new SingleStat();
            mana = new SingleStat();
        }

        private void InitData(MajorStatSO so)
        {
            damage = new SingleStat(so.damage);
            magicDamage = new SingleStat(so.magicDamage);
            health = new SingleStat(so.health);
            speed = new SingleStat(so.speed);
            armor = new SingleStat(so.armor);
            evasion = new SingleStat(so.evasion);
            mana = new SingleStat(so.mana);
        }

    }
}
