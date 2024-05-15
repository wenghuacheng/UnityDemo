using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// ���ȼ���Ч��
    /// </summary>
    public class HealthBarCut : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        //��Ѫ��ģ��
        [SerializeField] private Transform damageBarTemplate;

        //Ѫ���ؼ��ĳ���
        private const float BAR_WIDTH = 500;
        private HealthSystem healthSystem;

        private void Awake()
        {
            //Ѫ����ʼ��
            healthSystem = new HealthSystem(100);
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealthed += HealthSystem_OnHealthed;
            SetHealth(healthSystem.GetHealthNormalized());

        }

        private void Update()
        {
        }


        private void HealthSystem_OnHealthed()
        {
            SetHealth(healthSystem.GetHealthNormalized());
        }

        private void HealthSystem_OnDamaged()
        {
            var prevDamageBarFillAmount = barImage.fillAmount;

            SetHealth(healthSystem.GetHealthNormalized());

            //���ɵ�Ѫ����һ��Ѫ��
            Transform damageBar = Instantiate(damageBarTemplate, this.transform);
            damageBar.gameObject.SetActive(true);
            var rect = damageBar.GetComponent<RectTransform>();
            //���µ�Ѫ��Ϊ��ʼ������Ϊ���ٵ�Ѫ��
            rect.anchoredPosition = new Vector2(barImage.fillAmount * BAR_WIDTH, rect.anchoredPosition.y);
            damageBarTemplate.GetComponent<Image>().fillAmount = prevDamageBarFillAmount - barImage.fillAmount;
            damageBar.AddComponent<HealthBarCutFallDown>();
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