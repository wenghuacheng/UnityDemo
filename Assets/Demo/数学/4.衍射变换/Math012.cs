using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class Math012 : BaseMath
{
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// ͨ��������������ϵ
    /// ǰ����Ϊ����ϵ�Ļ�����
    /// ���һ��Ϊ����ϵԭ��
    /// </summary>
    private void Demo1()
    {
        //3D����ϵ
        var matrix1 = new float[4, 1] {
            {1},
            {2},
            {3},
            {0}
        };

        //2D����ϵ
        var matrix2 = new float[3, 1] {
            {1},
            {2},
            {0}
        };
    }
}
