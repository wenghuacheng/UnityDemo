using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI.QTE
{
    public class HitArea : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 50;//指针旋转速度
        [SerializeField] private RectTransform needleRect;//指针
        [SerializeField] private RectTransform gArea;//绿色区域
        [SerializeField] private RectTransform yLeftArea;//黄色区域
        [SerializeField] private RectTransform yRightArea;//黄色区域

        private float angle = 0;//当前角度
        private int rotateDirection = 1;//旋转方向

        private float startAngle;//区域起始位置
        private float yellowLeftAreaAngle;//左侧黄色区域角度
        private float greenAreaAngle;//中间绿色区域角度
        private float yellowRightAreaAngle;//有侧黄色区域角度

        private bool isRunning = false;

        void Start()
        {
            needleRect.rotation = Quaternion.identity;
            CreateArea();
            isRunning = true;
        }

        void Update()
        {
            AutoRotateNeedle();
            CheckArea();
        }

        /// <summary>
        /// 自动旋转指针
        /// </summary>
        private void AutoRotateNeedle()
        {
            if (!isRunning) return;

            if (angle <= -90)
                rotateDirection = 1;
            else if (angle >= 90)
                rotateDirection = -1;

            angle += rotationSpeed * Time.deltaTime * rotateDirection;
            needleRect.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        /// <summary>
        /// 确定指针区域
        /// </summary>
        private void CheckArea()
        {
            if (isRunning && Input.GetMouseButtonDown(0))
            {
                isRunning = false;

                //区域从0开始，旋转方向为负数方向
                //todo:这里向右旋转都是负数角度，后面考虑优化
                var lAreaStartAngle = -startAngle;
                var lAreaEndAngle = -(startAngle + yellowLeftAreaAngle);
                var gAreaEndAngle = -(startAngle + yellowLeftAreaAngle + greenAreaAngle);
                var rAreaEndAngle = -(startAngle + yellowLeftAreaAngle + greenAreaAngle + yellowRightAreaAngle);

                var checkAngle = angle - 90;//指针0在中间，所以需要校准

                Debug.Log(checkAngle);
                Debug.Log($"{lAreaStartAngle}-{lAreaEndAngle}-{gAreaEndAngle}-{rAreaEndAngle}");

                if (checkAngle <= lAreaStartAngle && checkAngle > lAreaEndAngle)
                {
                    Debug.Log("命中左侧黄色区域");
                }
                else if (checkAngle <= lAreaEndAngle && checkAngle > gAreaEndAngle)
                {
                    Debug.Log("命中绿色区域");
                }
                else if (checkAngle <= gAreaEndAngle && checkAngle > rAreaEndAngle)
                {
                    Debug.Log("命中右侧黄色区域");
                }
                else
                {
                    Debug.Log("未命中");
                }
            }
            else if(!isRunning && Input.GetMouseButtonDown(0))
            {
                isRunning = true;//重新开始
            }
        }

        #region 生成区域

        private void CreateArea()
        {
            //生成所有区域的角度
            yellowLeftAreaAngle = RamdomYAngle();
            yellowRightAreaAngle = RamdomYAngle();
            greenAreaAngle = RamdomGAngle();

            var allAngle = yellowLeftAreaAngle + yellowRightAreaAngle + greenAreaAngle;

            //设置显示区域的大小【基于角度显示那个部分】
            gArea.GetComponent<Image>().fillAmount = greenAreaAngle / 360f;
            yLeftArea.GetComponent<Image>().fillAmount = yellowLeftAreaAngle / 360f;
            yRightArea.GetComponent<Image>().fillAmount = yellowRightAreaAngle / 360f;

            startAngle = Random.Range(0, 180 - allAngle);
            //旋转相关的区域
            yLeftArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle));
            gArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle - yellowLeftAreaAngle));
            yRightArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle - yellowLeftAreaAngle - greenAreaAngle));
        }

        /// <summary>
        /// 获取绿色区域的生成角度
        /// </summary>
        private float RamdomGAngle()
        {
            return Random.Range(0.02f, 0.03f) * 360;
        }

        /// <summary>
        /// 获取黄色区域的生成角度
        /// </summary>
        private float RamdomYAngle()
        {
            return Random.Range(0.05f, 0.1f) * 360;
        }
        #endregion
    }
}