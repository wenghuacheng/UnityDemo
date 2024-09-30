using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// 进度减少效果
    /// </summary>
    public class HealthBarCut : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        //掉血的模板
        [SerializeField] private Transform damageBarTemplate;

        //血条控件的长度
        private const float BAR_WIDTH = 500;
        private HealthSystem healthSystem;

        private void Awake()
        {
            //血量初始化
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

            //生成掉血的那一段血条
            Transform damageBar = Instantiate(damageBarTemplate, this.transform);
            damageBar.gameObject.SetActive(true);
            var rect = damageBar.GetComponent<RectTransform>();
            //以新的血量为起始，长度为减少的血量
            rect.anchoredPosition = new Vector2(barImage.fillAmount * BAR_WIDTH, rect.anchoredPosition.y);
            damageBarTemplate.GetComponent<Image>().fillAmount = prevDamageBarFillAmount - barImage.fillAmount;
            damageBar.AddComponent<HealthBarCutFallDown>();
        }

        public void SetHealth(float healthNormalized)
        {
            //基于血量占比控制血条长度
            barImage.fillAmount = healthNormalized;
        }

        private void OnGUI()
        {
            //测试
            if (GUI.Button(new Rect(10, 10, 100, 50), "加血"))
            {
                healthSystem.Heal(10);
            }
            if (GUI.Button(new Rect(10, 60, 100, 50), "扣血"))
            {
                healthSystem.Damage(10);
            }
        }
    }
}