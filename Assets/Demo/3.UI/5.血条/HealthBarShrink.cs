using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// 进度减少效果
    /// </summary>
    public class HealthBarShrink : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private Image damageBarImage;

        //进度减少值
        private const float DECREASE_AMOUNT = 1f;
        //伤害条显示最大时间
        private const float DAMAGE_HEALTH_FADE_TIME_MAX = 1f;

        private HealthSystem healthSystem;

        private float damageBarFadeTime;

        private void Awake()
        {
            //血量初始化
            healthSystem = new HealthSystem(100);
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealthed += HealthSystem_OnHealthed;
            SetHealth(healthSystem.GetHealthNormalized());

            //重置伤害进度
            damageBarImage.fillAmount = barImage.fillAmount;

        }

        private void Update()
        {
            damageBarFadeTime -= Time.deltaTime;
            if (damageBarFadeTime < 0)
            {
                if (barImage.fillAmount < damageBarImage.fillAmount)
                {
                    //逐渐减少进度
                    damageBarImage.fillAmount -= DECREASE_AMOUNT * Time.deltaTime;
                    Debug.Log("--" + damageBarImage.fillAmount);
                }
            }
        }


        private void HealthSystem_OnHealthed()
        {
            SetHealth(healthSystem.GetHealthNormalized());
            //同步伤害进度
            damageBarImage.fillAmount = barImage.fillAmount;
        }

        private void HealthSystem_OnDamaged()
        {
            //重置伤害进度隐藏时间
            damageBarFadeTime = DAMAGE_HEALTH_FADE_TIME_MAX;
            SetHealth(healthSystem.GetHealthNormalized());
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