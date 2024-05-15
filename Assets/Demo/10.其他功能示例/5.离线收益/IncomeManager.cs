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
        private const int SecondIncome = 1;//每秒收入

        [SerializeField] private TextMeshProUGUI text;

        private float time;
        private float refreshTime = 1f;//定时刷新时间

        private int money = 0;//当前金额

        private DateTime offlineDate;

        private void Awake()
        {
            //获取离线金币与时间
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
        /// 计算离线收益
        /// </summary>
        /// <param name="offlineDate"></param>
        private int CalculateOffline(DateTime offlineDate)
        {
            //通过时间计算收益
            var timeSpan = DateTime.Now - offlineDate;
            int total = (int)timeSpan.TotalSeconds * SecondIncome;

            this.offlineDate = DateTime.Now;

            return total;
        }


        /// <summary>
        /// 每隔几秒刷新一次收益
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
            Debug.Log("程序退出了");
            money += CalculateOffline(offlineDate);
            PlayerPrefs.SetInt(MoneyKey, money);
            PlayerPrefs.SetString(OfflineDateKey, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

    }
}