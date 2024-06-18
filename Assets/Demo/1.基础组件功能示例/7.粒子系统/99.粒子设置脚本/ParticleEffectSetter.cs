using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PS
{
    /// <summary>
    /// 粒子参数的设置方法
    /// </summary>
    public class ParticleEffectSetter : MonoBehaviour
    {
        public void SetColorOverLifetimeSetting(ParticleSystem ps, ParticleEffectSettingSO setting)
        {
            var colorOverLifetimeModule = ps.colorOverLifetime;
            colorOverLifetimeModule.color = setting.colorGradient;
        }

        public void SetMainSetting(ParticleSystem ps, ParticleEffectSettingSO setting)
        {
            var mainModule = ps.main;
            mainModule.duration = setting.duration;
            mainModule.startSize = setting.startParticleSize;
            mainModule.startSpeed = setting.startParticleSpeed;
            mainModule.startLifetime = setting.startLifetime;
            mainModule.gravityModifier = setting.effectGravity;
            mainModule.maxParticles = setting.maxParticleNumber;
        }

        public void SetParticleEmission(ParticleSystem ps, ParticleEffectSettingSO setting)
        {
            var emissionModule = ps.emission;
            ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, setting.burstParticleNumber);
            emissionModule.SetBurst(0, burst);
            emissionModule.rateOverTime = setting.emissionRate;
        }

        public void SetParticleSprite(ParticleSystem ps, ParticleEffectSettingSO setting)
        {
            var sheetAnimationModule = ps.textureSheetAnimation;
            sheetAnimationModule.SetSprite(0, setting.sprite);
        }

        public void SetVelocityOverLifeTime(ParticleSystem ps, ParticleEffectSettingSO setting)
        {
            var velocityOverLifetimeModule = ps.velocityOverLifetime;

            var minMaxCurveX = new ParticleSystem.MinMaxCurve();
            minMaxCurveX.mode = ParticleSystemCurveMode.TwoConstants;
            minMaxCurveX.constantMin = setting.velocityOverLifetimeMin.x;
            minMaxCurveX.constantMax = setting.velocityOverLifetimeMax.x;
            velocityOverLifetimeModule.x = minMaxCurveX;

            var minMaxCurveY = new ParticleSystem.MinMaxCurve();
            minMaxCurveY.mode = ParticleSystemCurveMode.TwoConstants;
            minMaxCurveY.constantMin = setting.velocityOverLifetimeMin.y;
            minMaxCurveY.constantMax = setting.velocityOverLifetimeMax.y;
            velocityOverLifetimeModule.y = minMaxCurveY;

            var minMaxCurveZ = new ParticleSystem.MinMaxCurve();
            minMaxCurveZ.mode = ParticleSystemCurveMode.TwoConstants;
            minMaxCurveZ.constantMin = setting.velocityOverLifetimeMin.z;
            minMaxCurveZ.constantMax = setting.velocityOverLifetimeMax.z;
            velocityOverLifetimeModule.z = minMaxCurveZ;
        }
    }
}