using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI
{
    public class DamagePopup : MonoBehaviour
    {
        private TextMeshPro textMesh;
        //��ʧʱ��
        private float disappearTimer = 3f;
        //�������õ��ı���ɫ
        private Color textColor;
        //����Ʈ���ٶ�
        private float textMoveSpeed = 10f;
        //���ֵ����ٶ�
        private float disappearSpeed = 10;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshPro>();
            textColor = textMesh.color;
        }

        private void Update()
        {
            //��������Ʈ��
            transform.position += new Vector3(0, textMoveSpeed) * Time.deltaTime;

            //������ʧ
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
        /// �����˺�����
        /// </summary>
        /// <param name="damage"></param>
        public void Setup(string damage)
        {
            textMesh.SetText(damage.ToString());
        }

        /// <summary>
        /// ��������
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