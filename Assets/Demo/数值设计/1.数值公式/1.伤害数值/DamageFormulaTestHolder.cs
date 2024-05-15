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
         * 附加伤害
         *   -属性克制
         *   -暴击
         *   -真实伤害
         * **/

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Subtraction"))
            {
                var damage = DamageFormula.Subtraction(attack, defense);
                Debug.Log($"【减法公式】攻击力：{attack},防御力：{defense}，伤害值:{damage}");
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Multiplication"))
            {
                var damage = DamageFormula.Multiplication(attack, defense);
                Debug.Log($"【乘法公式】攻击力：{attack},防御力：{defense}，伤害值:{damage}");
            }
            if (GUI.Button(new Rect(0, 60, 100, 30), "Division"))
            {
                var damage = DamageFormula.Division(attack, defense);
                Debug.Log($"【除法公式】攻击力：{attack},防御力：{defense}，伤害值:{damage}");
            }
            if (GUI.Button(new Rect(0, 90, 100, 30), "Division2"))
            {
                var damage = DamageFormula.Division2(attack, defense);
                Debug.Log($"【除法公式2】攻击力：{attack},防御力：{defense}，伤害值:{damage}");
            }
        }

    }
}