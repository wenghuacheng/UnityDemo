using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Demo.UI
{
    public class RadarBg : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            CreateRadarChart(lineRenderer, 5);
        }

        private void CreateRadarChart(LineRenderer lineRenderer, int sectorCount)
        {
            float angleStep = 360f / sectorCount; // 扇形的角度步长
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = sectorCount + 1;
            lineRenderer.loop = true;

            int radius = 2;

            // 绘制扇形的顶点
            for (int i = 0; i <= sectorCount; i++)
            {
                float angle = i * angleStep;
                float x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                float y = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                Debug.Log(x + "," + y);
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }
    }
}