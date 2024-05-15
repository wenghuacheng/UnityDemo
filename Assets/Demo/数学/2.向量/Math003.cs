using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ӷ�
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
        //����1
        var v1 = new Vector3(1, 4);
        var line1 = CreateLineRenderer();
        line1.SetPositions(new Vector3[] { Vector3.zero, v1 });
        //����2
        var v2 = new Vector3(2, 1);
        var line2 = CreateLineRenderer();
        line2.SetPositions(new Vector3[] { Vector3.zero, v2 });
        //�ӷ������ƽ���ı��η��򣬽�����2�ƶ�������1��λ�ü��Ǽӷ������
        var v3 = v1 + v2;
        var line3 = CreateLineRenderer();
        line3.SetPositions(new Vector3[] { Vector3.zero, v3 });
    }
}
