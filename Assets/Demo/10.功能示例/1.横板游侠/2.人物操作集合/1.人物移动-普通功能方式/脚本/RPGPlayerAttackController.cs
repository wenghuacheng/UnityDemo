using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

namespace HB.Operation.Ability
{
    /// <summary>
    /// ��ҹ���
    /// </summary>
    public class RPGPlayerAttackController : MonoBehaviour
    {
        public bool IsAttack { get; private set; } = false;
        public int ComboCount { get; private set; } = 0;

        //�������ʱ��
        [SerializeField] private float comboDurationTime = 0.4f;

        private float comboTime = 0;

        public void Update()
        {
            Attack();
            AttackInterval();
        }

        //����
        public void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsAttack = true;
                comboTime = comboDurationTime;
            }
        }

        //��֡�������ã��ڹ������һ֡ʱ��֪��������
        public void AttackOver()
        {
            IsAttack = false;
            ComboCount++;
            //����ֵ�������
            if (ComboCount > 2)
            {
                ComboCount = 0;
            }
        }

        //�������,����ʱ������
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
