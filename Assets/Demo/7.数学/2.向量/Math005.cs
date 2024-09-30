using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向量点乘
/// </summary>
public class Math005 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        Dot();
    }

    private void Dot()
    {
        //向量1
        var v1 = new Vector3(1, 4);
        var line1 = CreateLineRenderer();
        line1.SetPositions(new Vector3[] { Vector3.zero, v1 });
        //向量2
        var v2 = new Vector3(2, 1);
        var line2 = CreateLineRenderer();
        line2.SetPositions(new Vector3[] { Vector3.zero, v2 });

        //打印点乘结果
        PrintDot(v1, v2);
        //通过点乘计算角度
        CalculateDegrees(v1, v2);
    }

    /// <summary>
    /// 点乘结果
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void PrintDot(Vector3 v1, Vector3 v2)
    {
        //>0,0-90
        //=0,90
        //<0,90-180
        var dot = Vector2.Dot(v1, v2);
        Debug.Log("点乘结果:" + dot);
    }

    /// <summary>
    /// 角度计算
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void CalculateDegrees(Vector3 v1, Vector3 v2)
    {
        var dot = Vector2.Dot(v1, v2);
        var cosRadian = dot / (v1.magnitude * v2.magnitude);
        var degrees = Mathf.Acos(cosRadian) * Mathf.Rad2Deg;
        Debug.Log("角度:" + degrees);
    }
}
