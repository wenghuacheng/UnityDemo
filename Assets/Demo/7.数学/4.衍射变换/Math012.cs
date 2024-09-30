using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 齐次坐标
/// </summary>
public class Math012 : BaseMath
{
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 通过矩阵描述坐标系
    /// 前三个为坐标系的基向量
    /// 最后一个为坐标系原点
    /// </summary>
    private void Demo1()
    {
        //3D坐标系
        var matrix1 = new float[4, 1] {
            {1},
            {2},
            {3},
            {0}
        };

        //2D坐标系
        var matrix2 = new float[3, 1] {
            {1},
            {2},
            {0}
        };
    }
}
