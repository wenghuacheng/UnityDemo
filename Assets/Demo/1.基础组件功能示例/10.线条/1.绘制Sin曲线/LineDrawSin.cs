using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class LineDrawSin : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private int positionCount = 720;
        private float xInterval = 0.05f;
        private float xOffest = 0;//x轴偏移量

        private float strength = 5;//增幅【如果是1则曲线幅度较小】


        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = positionCount;

            xOffest = positionCount * xInterval / 2;


        }

        void Update()
        {


        }

        private void FixedUpdate()
        {
            RefreshCurve();
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private int start = 0;
        private void RefreshCurve()
        {
            Vector3[] points = new Vector3[positionCount];

            start = (++start) % 360;
            for (int i = 0; i < positionCount; i++)
            {
                var y = Mathf.Sin((i + start) * Mathf.Deg2Rad) * strength;
                var x = i * xInterval - xOffest;//通过偏移让其居中
                points[i] = new Vector3(x, y, 0);
            }

            lineRenderer.SetPositions(points);
        }
    }
}
