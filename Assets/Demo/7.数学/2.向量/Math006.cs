using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向量叉乘
/// </summary>
public class Math006 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        Cross();
    }

    private void Cross()
    {
        //向量1
        var v1 = new Vector3(1, 4);
        var line1 = CreateLineRenderer();
        line1.SetPositions(new Vector3[] { Vector3.zero, v1 });
        //向量2
        var v2 = new Vector3(2, 1);
        var line2 = CreateLineRenderer();
        line2.SetPositions(new Vector3[] { Vector3.zero, v2 });

        //打印叉乘结果
        PrintCross(v1, v2);
        //计算角度
        CalculateDegrees(v1, v2);
    }

    /// <summary>
    /// 叉乘结果
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void PrintCross(Vector3 v1, Vector3 v2)
    {
        var dot = Vector3.Cross(v1, v2);
        Debug.Log("叉乘结果:" + dot);
    }

    /// <summary>
    /// 角度计算
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void CalculateDegrees(Vector3 v1, Vector3 v2)
    {
        var cross = Vector3.Cross(v1, v2);
        var sinRadian = cross.magnitude / (v1.magnitude * v2.magnitude);
        var degrees = Mathf.Asin(sinRadian) * Mathf.Rad2Deg;
        Debug.Log("角度:" + degrees);
    }
}
