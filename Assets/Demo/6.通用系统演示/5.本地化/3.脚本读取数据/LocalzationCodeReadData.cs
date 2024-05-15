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
    /// �����ȡ����
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

        #region ����1
        /// <summary>
        /// ͨ�����ƻ�ȡ���ݡ�������Ժ���Ҫ�ֶ�ˢ�¡�
        /// </summary>
        private void Method01()
        {
            //TableName�������� , EntryKey�����е�Key��
            var result = LocalizationSettings.StringDatabase.GetTableEntry("FirstLanguageTable", "Hello");
            text01.text = result.Entry.GetLocalizedString();
        }
        #endregion

        #region ����2
        /// <summary>
        /// �첽��ʽͨ�����ƻ�ȡ���ݡ���ֹ����ʱ��������δ��ʼ������������Ժ���Ҫ�ֶ�ˢ�¡�
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

        #region ����3
        /// <summary>
        /// ͨ�������¼������������Ժ���Զ�ˢ�¡�
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