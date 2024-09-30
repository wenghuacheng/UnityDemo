using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Demo.Common.Localzations
{
    /// <summary>
    /// ��̬�ı�
    /// </summary>
    public class LocalzationDynamicNameDemo : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private TextMeshProUGUI dynamicText;//��̬�ַ�����ʾ�ؼ�

        private void OnEnable()
        {
            //���������smart�е�{0} {1}����
            localizedString.Arguments = new object[] { "MyName", DateTime.Now.ToString("HH:mm:ss") };//���ö�̬�ı��еĲ���
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
            //�������ݷ����仯�����±��ػ�����
            localizedString.Arguments[1] = _date;//���ñ���Ĳ���ֵ
            localizedString.RefreshString();
        }
    }
}