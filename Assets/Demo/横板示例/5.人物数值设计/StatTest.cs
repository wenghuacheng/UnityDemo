using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    public class StatTest : MonoBehaviour
    {
        private CharacterStat stat;
        [SerializeField] private CharacterStat target;

        private void Awake()
        {
            stat = GetComponent<CharacterStat>();
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Attack"))
                Attack();
            else if (GUI.Button(new Rect(0, 30, 100, 30), "Hit"))
                Hit();
            else if (GUI.Button(new Rect(0, 60, 100, 30), "MagicAttack"))
                MagicAttack();
            else if (GUI.Button(new Rect(0, 90, 100, 30), "PrintPlayer"))
                PrintPlayer();
            else if (GUI.Button(new Rect(0, 120, 100, 30), "PrintTarget"))
                PrintTarget();
        }

        /// <summary>
        /// 普通攻击
        /// </summary>
        private void Attack()
        {
            stat.DoDamage(target);
        }

        /// <summary>
        /// 被命中
        /// </summary>
        private void Hit()
        {
            target.DoDamage(stat);
        }

        /// <summary>
        /// 魔法攻击
        /// </summary>
        private void MagicAttack()
        {
            stat.DoMagicDamage(target, MagicAttackType.fire);
        }

        /// <summary>
        /// 打印玩家信息
        /// </summary>
        private void PrintPlayer()
        {
            stat.Print();
        }

        /// <summary>
        /// 打印目标信息
        /// </summary>
        private void PrintTarget()
        {
            target.Print();
        }
    }
}