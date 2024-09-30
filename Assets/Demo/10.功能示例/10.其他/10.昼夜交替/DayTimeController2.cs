using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Demo.Misc
{
    /// <summary>
    /// 时间管理
    /// </summary>
    public class DayTimeController2 : MonoBehaviour
    {
        const float secondInOneDay = 86400f;//一天的秒数
        const float phaseLength = 900;//15分钟触发一次

        [SerializeField] private float timeScale = 1000f;//与实际时间的放大倍率

        private float time;
        private int days = 0;
        private float startAtTime = 28800f;//初始时间

        public List<TimeAgent> agents = new List<TimeAgent>();

        #region 属性
        float Hours
        {
            get { return time / 3600f; }
        }

        float Minutes
        {
            get { return time % 3600f / 60F; }
        }
        #endregion

        private void Start()
        {
            time = startAtTime;
        }

        void Update()
        {
            time += Time.deltaTime * timeScale;

            TimeValueCalculation();
            if (time > 86400f)
            {
                NextDay();
            }

            TimeAgents();
        }

        private void NextDay()
        {
            time = 0;
            days++;
        }

        private void TimeValueCalculation()
        {
            int hh = (int)Hours;
            int mm = (int)Minutes;
            var str = hh.ToString("00") + ":" + mm.ToString("00");//时间显示
        }

        int oldPhase = 0;
        private void TimeAgents()
        {
            int curPhase = (int)(time / phaseLength);

            if (oldPhase != curPhase)
            {
                oldPhase = curPhase;
                foreach (var agent in agents)
                {
                    agent.Invoke();
                }
            }
        }
    }

    //时间事件代理类
    public class TimeAgent : MonoBehaviour
    {
        public Action onTimeTick;//需要基于时间的事件

        public void Invoke()
        {
            onTimeTick?.Invoke();
        }
    }
}
