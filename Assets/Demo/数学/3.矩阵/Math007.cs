using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矩阵加法
/// </summary>
public class Math007 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        MatrixAdd();
    }

    private void MatrixAdd()
    {
        //使用四阶矩阵表示二阶，对角线是1，其他为0即可

        //矩阵加法需要两个矩阵都是同阶的

        //3*3矩阵
        Matrix4x4 m1 = new Matrix4x4(
            new Vector4(3, 9, -1, 0),
            new Vector4(4, 0, 2, 0),
            new Vector4(-4, 1, 7, 0),
            new Vector4(0, 0, 0, 1));

        //3*3的矩阵
        Matrix4x4 m2 = new Matrix4x4(
         new Vector4(3, 4, 9, 0),
         new Vector4(0, 1, 5, 0),
         new Vector4(7, 2, 6, 0),
         new Vector4(0, 0, 0, 1));

        //矩阵加法
        Matrix4x4 result = new Matrix4x4();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                result[i, j] = m1[i, j] + m2[i, j];

            }
        }

        //打印显示
        for (int i = 0; i < 4; i++)
        {
            var row = result.GetColumn(i);
            Debug.Log($"{row.x},{row.y},{row.z},{row.w}");
        }       
    }
}
