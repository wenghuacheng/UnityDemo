using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Demo.Common.Localzations
{
    /// <summary>
    /// 代码切换中英文
    /// </summary>
    public class LocalzationCodeSwitch : MonoBehaviour
    {
        private Locale chineseLocale;
        private Locale englishLocale;

        private AsyncOperationHandle<Locale> operationHandle;

        private void Awake()
        {
            operationHandle = LocalizationSettings.SelectedLocaleAsync;
            if (operationHandle.IsDone)
            {
                //打开就已经加载完毕
                CompletedOperationHandle(operationHandle);
            }
            else
            {
                //未加载完毕，通过事件触发
                operationHandle.Completed += CompletedOperationHandle;
            }
        }

        /// <summary>
        /// 本地化加载完毕
        /// </summary>
        /// <param name="obj"></param>
        private void CompletedOperationHandle(AsyncOperationHandle<Locale> obj)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;

            for (int i = 0; i < locales.Count; i++)
            {
                var locale = locales[i];

                //这里的名称是Project Settings->Localization中的Available Locales
                switch (locale.LocaleName)
                {
                    case "Chinese (Simplified) (zh-Hans)":
                        chineseLocale = locale;
                        break;
                    case "English (en)":
                        englishLocale = locale;
                        break;
                }

            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Chinese"))
            {
                LocalizationSettings.Instance.SetSelectedLocale(chineseLocale);
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "English"))
            {
                LocalizationSettings.Instance.SetSelectedLocale(englishLocale);
            }
        }
    }
}
