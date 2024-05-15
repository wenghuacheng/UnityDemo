using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矩阵乘法
/// </summary>
public class Math008 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        MatrixMultiple();
    }

    private void MatrixMultiple()
    {
        //矩阵乘法需要矩阵A的列维数=矩阵B的行位数

        //3*3矩阵
        Matrix4x4 m1 = new Matrix4x4(
            new Vector4(2, 0, 0, 0),
            new Vector4(-1, 4, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0));

        //3*3的矩阵
        Matrix4x4 m2 = new Matrix4x4(
         new Vector4(0.5f, -4, 0, 0),
         new Vector4(6, 7, 0, 0),
         new Vector4(0, 0, 0, 0),
         new Vector4(0, 0, 0, 0));

        //矩阵乘法[有问题，结果不对]
        Matrix4x4 result = m1 * m2;

        //打印显示
        for (int i = 0; i < 4; i++)
        {
            var row = result.GetColumn(i);
            Debug.Log($"{row.x},{row.y},{row.z},{row.w}");
        }
    }
}
