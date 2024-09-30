using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 齐次矩阵
/// </summary>
public class Math013 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        Demo1();
    }

    /// <summary>
    /// 2D坐标系中有一个绝对坐标系（0，0）。在当前矩阵中有一个点A（3，3）
    /// 在一个以（1，1）为坐标系的情况下,A的坐标会变成（3-1，3-1）
    /// 这里通过向量*矩阵的运算方式，转换点A的坐标系
    /// </summary>
    private void Demo1()
    {
        //通过矩阵运算方式转换点的坐标系
        Vector3 A = new Vector3(3, 3, 1);

        //新坐标系的(x,y)
        float cX = 1;
        float cY = 1;

        Matrix4x4 matrix4 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(cX, cY, 1, 0),
            new Vector4(0, 0, 0, 1)
            );

        //通过向量*矩阵的方式转换坐标系
        var result = matrix4.MultiplyVector(A);
        Debug.Log($"标准坐标系下的点:{A},新坐标系({cX},{cY})的点：{result}");
    }
}
