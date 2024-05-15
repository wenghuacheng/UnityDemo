using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Demo.Common.Localzations
{
    /// <summary>
    /// 动态文本
    /// </summary>
    public class LocalzationDynamicNameDemo : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private TextMeshProUGUI dynamicText;//动态字符串演示控件

        private void OnEnable()
        {
            //参数，针对smart中的{0} {1}参数
            localizedString.Arguments = new object[] { "MyName", DateTime.Now.ToString("HH:mm:ss") };//设置动态文本中的参数
            localizedString.StringChanged += LocalizedString_StringChanged;
        }

        private void OnDisable()
        {
            localizedString.StringChanged -= LocalizedString_StringChanged;
        }

        private void LocalizedString_StringChanged(string value)
        {
            dynamicText.text = value;
        }

        private void Update()
        {
            var _date = DateTime.Now.ToString("HH:mm:ss");
            //参数内容发生变化，更新本地化参数
            localizedString.Arguments[1] = _date;//设置变更的参数值
            localizedString.RefreshString();
        }
    }
}