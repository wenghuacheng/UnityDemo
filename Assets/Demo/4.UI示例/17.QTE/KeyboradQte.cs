using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UI.QTE
{
    public class KeyboradQte : MonoBehaviour
    {
        public const KeyCode keyCode = KeyCode.A;

        [SerializeField] private ProgressBar bar;
        [SerializeField] private float decreaseSpeed = 5;
        [SerializeField] private float increment = 0.2f;//一次点击时增加的量

        private float value;
        
        void Start()
        {
            value = 80;
        }

        void Update()
        {
            KeyPress();
            AutoDecrease();
            RefreshUI();
        }

        /// <summary>
        /// 用户按键
        /// </summary>
        private void KeyPress()
        {
            if (!Input.GetKeyDown(keyCode))
                return;

            this.value += increment;
        }

        /// <summary>
        /// 自动下降
        /// </summary>
        private void AutoDecrease()
        {
            this.value -= Time.deltaTime * decreaseSpeed;
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        private void RefreshUI()
        {
            bar.value = value;
        }
    }
}