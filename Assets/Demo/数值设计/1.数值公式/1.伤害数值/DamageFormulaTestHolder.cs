using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.ValueDesign.Damage
{
    public class DamageFormulaTestHolder : MonoBehaviour
    {
        [SerializeField] private float attack = 100;
        [SerializeField] private float defense = 30;

        /**
         * �����˺�
         *   -���Կ���
         *   -����
         *   -��ʵ�˺�
         * **/

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Subtraction"))
            {
                var damage = DamageFormula.Subtraction(attack, defense);
                Debug.Log($"��������ʽ����������{attack},��������{defense}���˺�ֵ:{damage}");
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Multiplication"))
            {
                var damage = DamageFormula.Multiplication(attack, defense);
                Debug.Log($"���˷���ʽ����������{attack},��������{defense}���˺�ֵ:{damage}");
            }
            if (GUI.Button(new Rect(0, 60, 100, 30), "Division"))
            {
                var damage = DamageFormula.Division(attack, defense);
                Debug.Log($"��������ʽ����������{attack},��������{defense}���˺�ֵ:{damage}");
            }
            if (GUI.Button(new Rect(0, 90, 100, 30), "Division2"))
            {
                var damage = DamageFormula.Division2(attack, defense);
                Debug.Log($"��������ʽ2����������{attack},��������{defense}���˺�ֵ:{damage}");
            }
        }

    }
}