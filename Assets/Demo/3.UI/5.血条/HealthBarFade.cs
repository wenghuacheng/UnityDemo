using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// ��������Ч��
    /// </summary>
    public class HealthBarFade : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private Image damageBarImage;

        //alpha������ֵ
        private const float FADE_AMOUNT = 5f;
        //�˺�����ʾ���ʱ��
        private const float DAMAGE_HEALTH_FADE_TIME_MAX = 1f;

        private HealthSystem healthSystem;

        private Color damageColor;
        private float damageBarFadeTime;

        private void Awake()
        {
            //Ѫ����ʼ��
            healthSystem = new HealthSystem(100);
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealthed += HealthSystem_OnHealthed;
            SetHealth(healthSystem.GetHealthNormalized());

            //damageBarImage.color.a = 0f;  //����ֱ���޸�������Ҫ������color���и�ֵ

            //��ʾ�˺���Ѫ��ͨ��alphaֵ����
            damageColor = damageBarImage.color;
            damageColor.a = 0f;
            damageBarImage.color = damageColor;
        }

        private void Update()
        {
            //���˺�����ʾһ��ʱ��󵭻�����
            if (damageColor.a > 0)
            {
                damageBarFadeTime -= Time.deltaTime;
                if (damageBarFadeTime < 0)
                {
                    damageColor.a -= FADE_AMOUNT * Time.deltaTime;
                    damageBarImage.color = damageColor;
                }
            }
        }

        private void HealthSystem_OnHealthed()
        {
            SetHealth(healthSystem.GetHealthNormalized());
        }

        private void HealthSystem_OnDamaged()
        {
            //�޸��˺�����ʾ���ȣ���ʾ�˺�Ѫ������������ʱ��
            if (damageColor.a <= 0)
            {
                damageBarImage.fillAmount = barImage.fillAmount;
            }
            damageColor.a = 1f;
            damageBarImage.color = damageColor;
            damageBarFadeTime = DAMAGE_HEALTH_FADE_TIME_MAX;

            SetHealth(healthSystem.GetHealthNormalized());
        }

        public void SetHealth(float healthNormalized)
        {
            //����Ѫ��ռ�ȿ���Ѫ������
            barImage.fillAmount = healthNormalized;
        }

        private void OnGUI()
        {
            //����
            if (GUI.Button(new Rect(10, 10, 100, 50), "��Ѫ"))
            {
                healthSystem.Heal(10);
            }
            if (GUI.Button(new Rect(10, 60, 100, 50), "��Ѫ"))
            {
                healthSystem.Damage(10);
            }
        }
    }
}