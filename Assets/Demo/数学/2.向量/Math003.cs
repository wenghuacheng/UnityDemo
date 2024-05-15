using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向量加法
/// </summary>
public class Math003 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        AddVector();
    }

    private void AddVector()
    {
        //向量1
        var v1 = new Vector3(1, 4);
        var line1 = CreateLineRenderer();
        line1.SetPositions(new Vector3[] { Vector3.zero, v1 });
        //向量2
        var v2 = new Vector3(2, 1);
        var line2 = CreateLineRenderer();
        line2.SetPositions(new Vector3[] { Vector3.zero, v2 });
        //加法结果【平行四边形法则，将向量2移动到向量1的位置即是加法结果】
        var v3 = v1 + v2;
        var line3 = CreateLineRenderer();
        line3.SetPositions(new Vector3[] { Vector3.zero, v3 });
    }
}
