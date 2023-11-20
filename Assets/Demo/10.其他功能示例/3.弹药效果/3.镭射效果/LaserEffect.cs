using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 需要后处理让激光更像一条射线，需要URP通用渲染管线。需要导入URP的包
/// </summary>
public class LaserEffect : MonoBehaviour
{
    private LineRenderer line;
    private bool isShooting = false;

    void Start()
    {
        this.line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        if (isShooting)
        {
            var mousePos = Input.mousePosition;
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            var direction = (mouseWorldPos - this.transform.position).normalized;

            //通过射线碰撞的坐标绘制镭射
            var hit = Physics2D.Raycast(this.transform.position, direction, 30);
            if (hit.collider != null)
            {
                line.positionCount = 2;
                line.SetPosition(0, this.transform.position);
                line.SetPosition(1, hit.point);
            }
        }
        else
        {
            line.positionCount = 0;
        }
    }
}
