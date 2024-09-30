using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.Sounds
{
    [CreateAssetMenu(fileName = "SoundEffectSO_", menuName = "测试SO/Sound")]
    public class SoundEffectSO : ScriptableObject
    {
        public string soundEffectName;

        public GameObject soundPrefab;

        public AudioClip soundEffectClip;

        //随机的音效范围
        [Range(0.1f, 1.5f)]
        public float soundEffectPitchRandomVariationMin = 0.8f;
        [Range(0.1f, 1.5f)]
        public float soundEffectPitchRandomVariationMax = 1.2f;

        //音量
        [Range(0f, 1f)]
        public float soundEffectVolume = 1f;
    }
}