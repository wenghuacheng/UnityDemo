using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    /// <summary>
    /// 角色属性
    /// </summary>
    [CreateAssetMenu(fileName = "RoleStat", menuName = "人物数值/角色数值", order = 4)]
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