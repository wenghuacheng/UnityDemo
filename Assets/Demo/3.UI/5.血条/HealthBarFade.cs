using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// 淡化隐藏效果
    /// </summary>
    public class HealthBarFade : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private Image damageBarImage;

        //alpha渐变数值
        private const float FADE_AMOUNT = 5f;
        //伤害条显示最大时间
        private const float DAMAGE_HEALTH_FADE_TIME_MAX = 1f;

        private HealthSystem healthSystem;

        private Color damageColor;
        private float damageBarFadeTime;

        private void Awake()
        {
            //血量初始化
            healthSystem = new HealthSystem(100);
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealthed += HealthSystem_OnHealthed;
            SetHealth(healthSystem.GetHealthNormalized());

            //damageBarImage.color.a = 0f;  //不能直接修改所以需要对整个color进行赋值

            //表示伤害的血条通过alpha值隐藏
            damageColor = damageBarImage.color;
            damageColor.a = 0f;
            damageBarImage.color = damageColor;
        }

        private void Update()
        {
            //当伤害条显示一段时间后淡化隐藏
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
            //修改伤害条显示进度，显示伤害血条，重置隐藏时间
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