using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class Speedometer : MonoBehaviour
    {
        //速度指针最大与最小时的角度
        private int needleMinAngle = 120;
        private int needleMaxAngle = -120;

        //当前速度
        private float speed = 0f;
        //最大速度
        private float maxSpeed = 0f;

        //指针
        [SerializeField] private GameObject needle;
        //用于生成刻度的UI模板
        [SerializeField] private GameObject markTemplate;

        private void Awake()
        {
            speed = 0f;
            maxSpeed = 200;
            //设置一个表盘有10个速度区间
            Draw(10);
        }

        void Update()
        {
            speed += Time.deltaTime * 30;
            if (speed > maxSpeed) speed = maxSpeed;
            RefreshSpeed();
        }

        /// <summary>
        /// 通过速度获取指针所在的角度
        /// </summary>
        /// <returns></returns>
        private float GetAngle()
        {
            //可旋转的角度
            int totalAngle = needleMinAngle - needleMaxAngle;
            //当前速度与最大速度的比例
            //当处于最大速度时则角度为needleMaxAngle，速度为0时则角度为needleMinAngle
            float speedNormalize = speed / maxSpeed;

            //此处需要结合界面上指针的旋转方向
            return needleMinAngle - speedNormalize * totalAngle;
        }

        /// <summary>
        /// 刷新速度指针
        /// </summary>
        private void RefreshSpeed()
        {
            needle.transform.eulerAngles = new Vector3(0, 0, GetAngle());
        }

        /// <summary>
        /// 画表盘面
        /// </summary>
        private void Draw(int labelCount)
        {
            for (int i = 0; i <= labelCount; i++)
            {
                //通过速度计算刻度的值(这里复用GetAngle函数)
                speed = (i / (float)labelCount) * maxSpeed;
                var angle = GetAngle();
                //生成一个刻度控件
                var markControl = Instantiate(markTemplate, transform);
                markControl.transform.eulerAngles = new Vector3(0, 0, angle);

                //找到名为SpeedText文本控件
                var textControl = markControl.transform.Find("SpeedText").GetComponent<Text>();
                //设置表盘的速度刻度
                textControl.text = speed.ToString();
                //将文字不会随着表盘旋转
                textControl.transform.eulerAngles = Vector3.zero;
                markControl.SetActive(true);
            }
            speed = 0;

            //将指针至于最上面，这样就不会被刻度盖着
            needle.transform.SetAsLastSibling();
        }
    }
}