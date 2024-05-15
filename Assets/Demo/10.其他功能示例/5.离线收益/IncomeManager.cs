using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Other.IncomeDemo
{
    [DisallowMultipleComponent]
    public class IncomeManager : MonoBehaviour
    {
        private const string OfflineDateKey = "OfflineDate";
        private const string MoneyKey = "Money";
        private const int SecondIncome = 1;//ÿ������

        [SerializeField] private TextMeshProUGUI text;

        private float time;
        private float refreshTime = 1f;//��ʱˢ��ʱ��

        private int money = 0;//��ǰ���

        private DateTime offlineDate;

        private void Awake()
        {
            //��ȡ���߽����ʱ��
            money = PlayerPrefs.GetInt(MoneyKey, 0);
            var dateTimeStr = PlayerPrefs.GetString(OfflineDateKey);
            if (!string.IsNullOrWhiteSpace(dateTimeStr))
            {
                offlineDate = DateTime.Parse(dateTimeStr);
                money += CalculateOffline(offlineDate);
            }
            else
            {
                offlineDate = DateTime.Now;
            }


            Application.quitting += Application_quitting;
        }

        private void Update()
        {
            RefreshIncome();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="offlineDate"></param>
        private int CalculateOffline(DateTime offlineDate)
        {
            //ͨ��ʱ���������
            var timeSpan = DateTime.Now - offlineDate;
            int total = (int)timeSpan.TotalSeconds * SecondIncome;

            this.offlineDate = DateTime.Now;

            return total;
        }


        /// <summary>
        /// ÿ������ˢ��һ������
        /// </summary>
        private void RefreshIncome()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = refreshTime;
                money += CalculateOffline(offlineDate);
                text.text = money.ToString();
            }
        }


        private void Application_quitting()
        {
            Debug.Log("�����˳���");
            money += CalculateOffline(offlineDate);
            PlayerPrefs.SetInt(MoneyKey, money);
            PlayerPrefs.SetString(OfflineDateKey, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

    }
}