using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Demo.Misc
{
    /// <summary>
    /// ʱ�����
    /// </summary>
    public class DayTimeController2 : MonoBehaviour
    {
        const float secondInOneDay = 86400f;//һ�������
        const float phaseLength = 900;//15���Ӵ���һ��

        [SerializeField] private float timeScale = 1000f;//��ʵ��ʱ��ķŴ���

        private float time;
        private int days = 0;
        private float startAtTime = 28800f;//��ʼʱ��

        public List<TimeAgent> agents = new List<TimeAgent>();

        #region ����
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
            var str = hh.ToString("00") + ":" + mm.ToString("00");//ʱ����ʾ
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

    //ʱ���¼�������
    public class TimeAgent : MonoBehaviour
    {
        public Action onTimeTick;//��Ҫ����ʱ����¼�

        public void Invoke()
        {
            onTimeTick?.Invoke();
        }
    }
}
