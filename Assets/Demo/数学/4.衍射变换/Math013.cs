using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ξ���
/// </summary>
public class Math013 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        Demo1();
    }

    /// <summary>
    /// 2D����ϵ����һ����������ϵ��0��0�����ڵ�ǰ��������һ����A��3��3��
    /// ��һ���ԣ�1��1��Ϊ����ϵ�������,A��������ɣ�3-1��3-1��
    /// ����ͨ������*��������㷽ʽ��ת����A������ϵ
    /// </summary>
    private void Demo1()
    {
        //ͨ���������㷽ʽת���������ϵ
        Vector3 A = new Vector3(3, 3, 1);

        //������ϵ��(x,y)
        float cX = 1;
        float cY = 1;

        Matrix4x4 matrix4 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(cX, cY, 1, 0),
            new Vector4(0, 0, 0, 1)
            );

        //ͨ������*����ķ�ʽת������ϵ
        var result = matrix4.MultiplyVector(A);
        Debug.Log($"��׼����ϵ�µĵ�:{A},������ϵ({cX},{cY})�ĵ㣺{result}");
    }
}
