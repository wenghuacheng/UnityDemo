using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI
{
    public class DamagePopup : MonoBehaviour
    {
        private TextMeshPro textMesh;
        //消失时间
        private float disappearTimer = 3f;
        //用于设置的文本颜色
        private Color textColor;
        //文字飘动速度
        private float textMoveSpeed = 10f;
        //文字淡化速度
        private float disappearSpeed = 10;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshPro>();
            textColor = textMesh.color;
        }

        private void Update()
        {
            //文字向上飘动
            transform.position += new Vector3(0, textMoveSpeed) * Time.deltaTime;

            //淡化消失
            if (disappearTimer > 0)
                disappearTimer -= Time.deltaTime;
            else
            {
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// 设置伤害文字
        /// </summary>
        /// <param name="damage"></param>
        public void Setup(string damage)
        {
            textMesh.SetText(damage.ToString());
        }

        /// <summary>
        /// 生成文字
        /// </summary>
        /// <param name="damagePopupTemplate"></param>
        /// <returns></returns>
        public static DamagePopup Create(Transform damagePopupTemplate, Vector3 position, int damage)
        {
            var damagePopupTransform = Instantiate(damagePopupTemplate, position, Quaternion.identity);
            var damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.Setup(damage.ToString());
            return damagePopup;
        }
    }
}