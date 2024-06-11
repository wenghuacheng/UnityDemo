using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Basic.Sounds
{
    public class SoundEffectManager : MonoBehaviour
    {
        public int soundsVolume = 8;

        private void Start()
        {
            SetSoundsVolume(soundsVolume);
        }

        public void SetSoundsVolume(int soundsVolume)
        {
            this.soundsVolume = soundsVolume;

            float muteDecibels = -80f;//Ĭ���������ֵ

            if (soundsVolume == 0)
            {
                GameManager.Instance.mixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
            }
            else
            {
                //���ڻ�������������С����0����-80����Ҫ����ת��
                float linearScaleRange = 20f;
                var volume = Mathf.Log10((float)soundsVolume / linearScaleRange) * 20f;

                GameManager.Instance.mixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
            }
        }

        public void PlaySoundEffect(SoundEffectSO soundEffect)
        {
            //todo����Ҫ��Ϊ�����
            var component = Instantiate(soundEffect.soundPrefab);        
            var sound = component.GetComponent<SoundEffect>();
            sound.gameObject.SetActive(false);
            sound.SetSound(soundEffect);
            sound.gameObject.SetActive(true);

            StartCoroutine(DisableSound(sound,soundEffect.soundEffectClip.length));
        }

        /// <summary>
        /// ������ɺ�ֹͣ
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="soundDuration"></param>
        /// <returns></returns>
        private IEnumerator DisableSound(SoundEffect sound,float soundDuration)
        {
            yield return new WaitForSeconds(soundDuration);
            sound.gameObject.SetActive(false);
        }
    }
}