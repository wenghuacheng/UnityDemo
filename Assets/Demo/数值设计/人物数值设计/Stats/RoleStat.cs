using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public class RoleStat
    {
        public RoleStat()
        {
            InitData();
        }

        public RoleStat(RoleStatSO so)
        {
            InitData(so);
        }

        //力量
        public SingleStat strength;
        //敏捷
        public SingleStat agility;
        //智力
        public SingleStat intelligence;
        //耐力
        public SingleStat stamina;
        //幸运
        public SingleStat luck;
        //韧性
        public SingleStat resilience;

        private void InitData()
        {
            strength = new SingleStat();
            agility = new SingleStat();
            intelligence = new SingleStat();
            stamina = new SingleStat();
            luck = new SingleStat();
            resilience = new SingleStat();
        }

        private void InitData(RoleStatSO so)
        {
            strength = new SingleStat(so.strength);
            agility = new SingleStat(so.agility);
            intelligence = new SingleStat(so.intelligence);
            stamina = new SingleStat(so.stamina);
            luck = new SingleStat(so.luck);
            resilience = new SingleStat(so.resilience);
        }

    }
}