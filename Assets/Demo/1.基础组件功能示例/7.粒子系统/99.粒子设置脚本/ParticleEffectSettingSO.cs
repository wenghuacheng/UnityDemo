using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PS
{
    /// <summary>
    /// 粒子参数设置
    /// 通过该方式可以使粒子对象使用对象池，获取对象后修改相关的配置即可使用
    /// </summary>
    public class ParticleEffectSettingSO : ScriptableObject
    {
        //基础设置
        public float duration = 0.5f;
        public float startParticleSize = 0.25f;
        public float startParticleSpeed = 3f;
        public float startLifetime = 0.5f;
        public int maxParticleNumber = 100;
        public float effectGravity = -0.01f;

        //Emission
        public int emissionRate = 100;
        public int burstParticleNumber = 20;

        //Color over Lifetime
        public Gradient colorGradient;

        //TextureSheetAnimation
        public Sprite sprite;

        //velocityOverLifetime
        public Vector3 velocityOverLifetimeMin;
        public Vector3 velocityOverLifetimeMax;

        public GameObject weaponShootEffectPrefab;
    }
}
