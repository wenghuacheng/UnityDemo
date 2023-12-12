using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Demo.Common.Locailzations
{
    public class LocailzationManager : MonoBehaviour
    {
        private bool isSwitching = false;

        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private TextMeshProUGUI dynamicText;//动态字符串演示控件

        private string _name = "ZhangSan";
        private string _date;

        private float time;

        #region 动态文本
        private void OnEnable()
        {
            SetLocale(0);
            localizedString.Arguments = new object[] { _name, _date };//设置动态文本中的参数
            localizedString.StringChanged += LocalizedString_StringChanged;
        }

        private void Update()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time += 1;
                _date = DateTime.Now.ToString("HH:mm:ss");

                //参数内容发生变化，更新本地化参数
                localizedString.Arguments[1] = _date;//设置变更的参数值
                localizedString.RefreshString();
            }
        }

        private void OnDisable()
        {
            localizedString.StringChanged -= LocalizedString_StringChanged;
        }

        /// <summary>
        /// 文笔变更事件
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void LocalizedString_StringChanged(string value)
        {
            dynamicText.text = value;
        }
        #endregion


        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 60), "切换为中文"))
            {
                SetLocale(0);
            }
            if (GUI.Button(new Rect(0, 60, 200, 60), "切换为英文"))
            {
                SetLocale(1);
            }
        }

        #region 切换语言
        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="localeId"></param>
        private void SetLocale(int localeId)
        {
            if (isSwitching)
                return;

            StartCoroutine(SetLocaleAsync(localeId));
        }

        /// <summary>
        /// 切换语言(异步)
        /// </summary>
        /// <param name="localeId"></param>
        /// <returns></returns>
        private IEnumerator SetLocaleAsync(int localeId)
        {
            isSwitching = true;
            yield return LocalizationSettings.InitializationOperation;//初始化配置
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];//在Project Setting中生成的语言顺序
            isSwitching = false;
        }
        #endregion
    }
}
