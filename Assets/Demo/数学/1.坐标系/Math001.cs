using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 极坐标转换为笛卡尔坐标系
/// </summary>
public class Math001 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        PolarToDescartes();
    }

    /// <summary>
    /// 极坐标转换为笛卡尔坐标系
    /// </summary>
    private void PolarToDescartes()
    {
        for (int i = 0; i < 7; i++)
        {
            var line = CreateLineRenderer();
            float degrees = 45 * i;
            float radians = degrees * Mathf.Deg2Rad;
            //通过与X轴的角度转换为笛卡尔坐标（X,Y）
            var x = MathF.Cos(radians);
            var y = MathF.Sin(radians);

            line.SetPositions(new Vector3[] { Vector3.zero, new Vector3(x, y) });
        }
    }
}
