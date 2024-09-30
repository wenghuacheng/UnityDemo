using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����˷�
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
        //����˷���Ҫ����A����ά��=����B����λ��

        //3*3����
        Matrix4x4 m1 = new Matrix4x4(
            new Vector4(2, 0, 0, 0),
            new Vector4(-1, 4, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0));

        //3*3�ľ���
        Matrix4x4 m2 = new Matrix4x4(
         new Vector4(0.5f, -4, 0, 0),
         new Vector4(6, 7, 0, 0),
         new Vector4(0, 0, 0, 0),
         new Vector4(0, 0, 0, 0));

        //����˷�[�����⣬�������]
        Matrix4x4 result = m1 * m2;

        //��ӡ��ʾ
        for (int i = 0; i < 4; i++)
        {
            var row = result.GetColumn(i);
            Debug.Log($"{row.x},{row.y},{row.z},{row.w}");
        }
    }
}
