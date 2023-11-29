using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI.QTE
{
    /// <summary>
    /// 文本提示
    /// </summary>
    public class QuickKeyTxtUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt;
        [SerializeField] private TextMeshProUGUI shadowTxt;

        private float minFontSize = 20;
        private float maxFontSize = 40;
        private float currentFontSize = 0;

        private float hiddenTime = 0f;

        void Start()
        {
            txt.fontSize = minFontSize;
            currentFontSize = txt.fontSize;
        }

        void Update()
        {
            AutoDecrease();
            KeyPress();
            RefreshFontSize();
            HiddenShadowText();
        }

        private void KeyPress()
        {
            if (!Input.GetKeyDown(QuickKeyPress.keyCode))
                return;

            currentFontSize += 10f;

            //激活阴影
            hiddenTime = 0.1f;
            shadowTxt.fontSize = txt.fontSize * 1.5f;
            shadowTxt.gameObject.SetActive(true);
        }

        /// <summary>
        /// 自动下降
        /// </summary>
        private void AutoDecrease()
        {
            currentFontSize -= Time.deltaTime * 10;
        }

        /// <summary>
        /// 刷新字号
        /// </summary>
        private void RefreshFontSize()
        {
            if (currentFontSize < minFontSize)
                currentFontSize = minFontSize;
            else if (currentFontSize > maxFontSize)
                currentFontSize = maxFontSize;

            txt.fontSize = currentFontSize;

        }

        /// <summary>
        /// 自动隐藏阴影文本
        /// </summary>
        private void HiddenShadowText()
        {
            hiddenTime -= Time.deltaTime;
            if (hiddenTime <= 0)
                shadowTxt.gameObject.SetActive(false);
        }
    }
}