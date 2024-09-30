using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Demo.Misc
{
    public class DayTimeController : MonoBehaviour
    {
        const float secondInOneDay = 86400f;//一天的秒数

        [SerializeField] private Color nightLightColor;
        [SerializeField] private Color dayLightColor = Color.white;
        [SerializeField] AnimationCurve nightTimeCurve;

        [SerializeField] private float timeScale = 1000f;//与实际时间的放大倍率
        [SerializeField] private Light2D globalLight;

        [SerializeField] private TextMeshProUGUI text;

        private float time;
        private int days = 0;

        float Hours
        {
            get { return time / 3600f; }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime * timeScale;
            text.text = Hours.ToString();

            float v=nightTimeCurve.Evaluate(Hours);
            Debug.Log(v);
            Color c = Color.Lerp(dayLightColor, nightLightColor, v);
            globalLight.color = c;

            if (time > 86400f)
            {
                NextDay();
            }
        }

        private void NextDay()
        {
            time = 0;
            days++;
        }
    }
}