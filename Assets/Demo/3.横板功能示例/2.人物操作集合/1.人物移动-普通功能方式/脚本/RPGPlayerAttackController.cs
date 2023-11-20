using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

namespace HB.Operation.Ability
{
    /// <summary>
    /// 玩家攻击
    /// </summary>
    public class RPGPlayerAttackController : MonoBehaviour
    {
        public bool IsAttack { get; private set; } = false;
        public int ComboCount { get; private set; } = 0;

        //连击间隔时间
        [SerializeField] private float comboDurationTime = 0.4f;

        private float comboTime = 0;

        public void Update()
        {
            Attack();
            AttackInterval();
        }

        //攻击
        public void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsAttack = true;
                comboTime = comboDurationTime;
            }
        }

        //由帧动画调用，在攻击最后一帧时告知攻击结束
        public void AttackOver()
        {
            IsAttack = false;
            ComboCount++;
            //连击值最大三次
            if (ComboCount > 2)
            {
                ComboCount = 0;
            }
        }

        //攻击间隔,超过时间重置
        private void AttackInterval()
        {
            comboTime -= Time.deltaTime;
            if (comboTime < 0)
            {
                IsAttack = false;
                ComboCount = 0;
            }
        }
    }
}
