using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    public class DamageManager : MonoBehaviour
    {
        public static DamageManager Instance;

        [SerializeField] private DamageText damageTextPrefab;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// µ¯³öÉËº¦ÎÄ×Ö
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <param name="parent"></param>
        public void ShowDamageText(float damageAmount, Transform parent)
        {
            var text = Instantiate(damageTextPrefab, parent);
            text.transform.position += Vector3.right * 0.5f;//Õë¶Ô¸¸¿Ø¼þÆ«ÒÆ
            text.SetDamageText(damageAmount);
        }
    }
}