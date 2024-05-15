using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矩阵与向量转换意义
/// </summary>
public class Math011 : BaseMath
{
    protected override void Start()
    {
        base.Start();

        DrawMatrix();
    }

    /// <summary>
    /// 将矩阵拆解为向量
    /// 其绘制两个向量相当于（1，0）和（0，1）进行旋转和缩放
    /// </summary>
    private void DrawMatrix()
    {
        var matrix = new float[2, 2] {
            { 3,1},
            { -1,3}
        };

        //拆解为向量(3,1)
        Vector2 v1 = new Vector2(matrix[0, 0], matrix[0, 1]);
        var l1 = CreateLineRenderer();
        l1.SetPositions(new Vector3[] { Vector3.zero, v1 });

        //拆解为向量(-1,3)
        Vector2 v2 = new Vector2(matrix[1, 0], matrix[1, 1]);
        var l2 = CreateLineRenderer();
        l2.SetPositions(new Vector3[] { Vector3.zero, v2 });
    }

}
