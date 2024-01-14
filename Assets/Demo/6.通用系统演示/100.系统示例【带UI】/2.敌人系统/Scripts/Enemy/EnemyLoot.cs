using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 敌人掉落相关
    /// 【目前不添加演示，可以通过单例的GameManager的方式在击杀后给玩家添加经验/物品】
    /// </summary>
    public class EnemyLoot : MonoBehaviour
    {
        [Header("Config")]
        private float expDrop;

        //击杀后所得经验
        public float ExpDrop => expDrop;
    }
}