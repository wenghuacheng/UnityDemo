using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Demo.Common.Localzations
{
    /// <summary>
    /// 代码读取数据
    /// </summary>
    public class LocalzationCodeReadData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text01;
        [SerializeField] private TextMeshProUGUI text02;
        [SerializeField] private TextMeshProUGUI text03;

        private void Awake()
        {
            Method01();
            StartCoroutine(Method02());
        }

        #region 方法1
        /// <summary>
        /// 通过名称获取数据【变更语言后需要手动刷新】
        /// </summary>
        private void Method01()
        {
            //TableName【表名】 , EntryKey【表中的Key】
            var result = LocalizationSettings.StringDatabase.GetTableEntry("FirstLanguageTable", "Hello");
            text01.text = result.Entry.GetLocalizedString();
        }
        #endregion

        #region 方法2
        /// <summary>
        /// 异步方式通过名称获取数据【防止加载时语言配置未初始化】【变更语言后需要手动刷新】
        /// </summary>
        private IEnumerator Method02()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync("FirstLanguageTable");
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                text02.text = stringTable.GetEntry("Hello").GetLocalizedString();
            }
        }
        #endregion

        #region 方法3
        /// <summary>
        /// 通过监听事件变更【变更语言后会自动刷新】
        /// </summary>
        private LocalizedStringTable stringTable = new LocalizedStringTable() { TableReference = "FirstLanguageTable" };

        private void OnEnable()
        {
            stringTable.TableChanged += OnTableChanged;
        }

        private void OnDisable()
        {
            stringTable.TableChanged -= OnTableChanged;
        }

        private void OnTableChanged(StringTable stringTable)
        {
            var entry = stringTable.GetEntry("Hello");
            text03.text = entry.GetLocalizedString();
        }

        #endregion

    }
}