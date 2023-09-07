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
        }

        /// <summary>
        /// ÆÕÍ¨¹¥»÷
        /// </summary>
        private void Attack()
        {
            stat.DoDamage(target);
        }

        /// <summary>
        /// ±»ÃüÖÐ
        /// </summary>
        private void Hit()
        {
            target.DoDamage(stat);
        }

        /// <summary>
        /// Ä§·¨¹¥»÷
        /// </summary>
        private void MagicAttack()
        {
            stat.DoMagicDamage(target, MagicAttackType.fire);
        }
    }
}