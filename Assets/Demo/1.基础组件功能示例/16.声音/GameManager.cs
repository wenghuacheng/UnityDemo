using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Demo.Basic.Sounds
{
    public class GameManager : MonoBehaviour
    {
        public AudioMixerGroup mixerGroup;
        public static GameManager Instance;

        [SerializeField] private SoundEffectSO[] list;
        [SerializeField] private SoundEffectManager soundEffectManager;

        private void Awake()
        {
            Instance = this;
        }


        #region ≤‚ ‘

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Sound001"))
            {
                soundEffectManager.PlaySoundEffect(list[0]);
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Sound002"))
            {
                soundEffectManager.PlaySoundEffect(list[1]);
            }
        }

        #endregion
    }
}