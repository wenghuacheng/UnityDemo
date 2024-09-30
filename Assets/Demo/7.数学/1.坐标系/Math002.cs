using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 笛卡尔坐标转为极坐标
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
    /// 笛卡尔坐标转为极坐标（角度）
    /// </summary>
    private void DescartesToPolar()
    {
        var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //计算角度
        /**
         第一象限是0-90度
         第二象限是（-90）-0度
         第三象限是0-90度
         第四象限是（-90）-0度
         */
        var radian = Mathf.Atan(pos.y / pos.x);
        var degrees = radian * Mathf.Rad2Deg;
        Debug.Log(degrees);

        line.SetPositions(new Vector3[] { Vector3.zero, pos });
    }
}
