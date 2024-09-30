using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ת��Ϊ�ѿ�������ϵ
/// </summary>
public class Math001 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        PolarToDescartes();
    }

    /// <summary>
    /// ������ת��Ϊ�ѿ�������ϵ
    /// </summary>
    private void PolarToDescartes()
    {
        for (int i = 0; i < 7; i++)
        {
            var line = CreateLineRenderer();
            float degrees = 45 * i;
            float radians = degrees * Mathf.Deg2Rad;
            //ͨ����X��ĽǶ�ת��Ϊ�ѿ������꣨X,Y��
            var x = MathF.Cos(radians);
            var y = MathF.Sin(radians);

            line.SetPositions(new Vector3[] { Vector3.zero, new Vector3(x, y) });
        }
    }
}
