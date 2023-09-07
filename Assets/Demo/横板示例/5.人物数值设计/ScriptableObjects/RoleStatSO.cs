using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    /// <summary>
    /// 角色属性
    /// </summary>
    [CreateAssetMenu(fileName = "RoleStat", menuName = "Stat/RoleStat", order = 4)]
    public class RoleStatSO : ScriptableObject
    {
        //力量
        public int strength;
        //敏捷
        public int agility;
        //智力
        public int intelligence;
        //耐力
        public int stamina;
        //幸运
        public int luck;
        //韧性
        public int resilience;
    }
}