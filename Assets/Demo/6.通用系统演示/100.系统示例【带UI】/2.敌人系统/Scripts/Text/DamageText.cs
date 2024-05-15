using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ÉËº¦ÎÄ×Ö
    /// </summary>
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI damageTMP;

        public void SetDamageText(float damage)
        {
            damageTMP.text = damage.ToString();
        }

        public void DestoryText()
        {
            Destroy(this.gameObject);
        }
    }
}