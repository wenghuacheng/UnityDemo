using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// ���ȼ���Ч��
    /// </summary>
    public class HealthBarShrink : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private Image damageBarImage;

        //���ȼ���ֵ
        private const float DECREASE_AMOUNT = 1f;
        //�˺�����ʾ���ʱ��
        private const float DAMAGE_HEALTH_FADE_TIME_MAX = 1f;

        private HealthSystem healthSystem;

        private float damageBarFadeTime;

        private void Awake()
        {
            //Ѫ����ʼ��
            healthSystem = new HealthSystem(100);
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealthed += HealthSystem_OnHealthed;
            SetHealth(healthSystem.GetHealthNormalized());

            //�����˺�����
            damageBarImage.fillAmount = barImage.fillAmount;

        }

        private void Update()
        {
            damageBarFadeTime -= Time.deltaTime;
            if (damageBarFadeTime < 0)
            {
                if (barImage.fillAmount < damageBarImage.fillAmount)
                {
                    //�𽥼��ٽ���
                    damageBarImage.fillAmount -= DECREASE_AMOUNT * Time.deltaTime;
                    Debug.Log("--" + damageBarImage.fillAmount);
                }
            }
        }


        private void HealthSystem_OnHealthed()
        {
            SetHealth(healthSystem.GetHealthNormalized());
            //ͬ���˺�����
            damageBarImage.fillAmount = barImage.fillAmount;
        }

        private void HealthSystem_OnDamaged()
        {
            //�����˺���������ʱ��
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