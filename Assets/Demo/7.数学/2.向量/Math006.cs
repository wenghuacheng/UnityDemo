using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class Math006 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        Cross();
    }

    private void Cross()
    {
        //����1
        var v1 = new Vector3(1, 4);
        var line1 = CreateLineRenderer();
        line1.SetPositions(new Vector3[] { Vector3.zero, v1 });
        //����2
        var v2 = new Vector3(2, 1);
        var line2 = CreateLineRenderer();
        line2.SetPositions(new Vector3[] { Vector3.zero, v2 });

        //��ӡ��˽��
        PrintCross(v1, v2);
        //����Ƕ�
        CalculateDegrees(v1, v2);
    }

    /// <summary>
    /// ��˽��
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void PrintCross(Vector3 v1, Vector3 v2)
    {
        var dot = Vector3.Cross(v1, v2);
        Debug.Log("��˽��:" + dot);
    }

    /// <summary>
    /// �Ƕȼ���
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    private void CalculateDegrees(Vector3 v1, Vector3 v2)
    {
        var cross = Vector3.Cross(v1, v2);
        var sinRadian = cross.magnitude / (v1.magnitude * v2.magnitude);
        var degrees = Mathf.Asin(sinRadian) * Mathf.Rad2Deg;
        Debug.Log("�Ƕ�:" + degrees);
    }
}
