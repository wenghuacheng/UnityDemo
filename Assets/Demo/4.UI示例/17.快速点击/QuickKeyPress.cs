using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI.QTE
{
    public class QuickKeyPress : MonoBehaviour
    {
        public const KeyCode keyCode = KeyCode.A;

        [SerializeField] private Image bar;
        [SerializeField] private float decreaseSpeed = 2f;//默认下降速度
        [SerializeField] private float increment = 1f;//一次点击时增加的量

        private float value;

        #region 初始化
        void Start()
        {
            value = 80;
            RefreshUI();
        }

        #endregion

        void Update()
        {
            AutoDecrease();
            KeyPress();
            RefreshUI();
            CheckValue();
        }

        /// <summary>
        /// 用户按键
        /// </summary>
        private void KeyPress()
        {
            if (!Input.GetKeyDown(keyCode))
                return;

            this.value += increment;
            Debug.Log(this.value);
        }

        /// <summary>
        /// 自动下降
        /// </summary>
        private void AutoDecrease()
        {
            this.value -= Time.deltaTime * decreaseSpeed;
        }

        /// <summary>
        /// 判定结果
        /// </summary>
        private void CheckValue()
        {
            if (value >= 100)
            {
                Debug.Log("赢了");
                Destroy(this.gameObject);
            }
            else if (value <= 0)
            {
                Debug.Log("输了");
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        private void RefreshUI()
        {
            bar.fillAmount = value / 100f;
        }
    }
}