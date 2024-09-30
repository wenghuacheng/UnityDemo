using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �ѿ�������תΪ������
/// </summary>
public class Math002 : BaseMath
{
    private Camera mainCamera;
    private LineRenderer line;

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        line = CreateLineRenderer();
    }

    private void Update()
    {
        DescartesToPolar();
    }

    /// <summary>
    /// �ѿ�������תΪ�����꣨�Ƕȣ�
    /// </summary>
    private void DescartesToPolar()
    {
        var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //����Ƕ�
        /**
         ��һ������0-90��
         �ڶ������ǣ�-90��-0��
         ����������0-90��
         ���������ǣ�-90��-0��
         */
        var radian = Mathf.Atan(pos.y / pos.x);
        var degrees = radian * Mathf.Rad2Deg;
        Debug.Log(degrees);

        line.SetPositions(new Vector3[] { Vector3.zero, pos });
    }
}
