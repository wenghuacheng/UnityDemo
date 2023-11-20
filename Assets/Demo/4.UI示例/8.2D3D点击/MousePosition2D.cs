using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition2D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        //转换为世界坐标
        var p = mainCamera.ScreenToWorldPoint(Input.mousePosition);      
        //注意Z轴，直接使用摄像机的Z轴会看不见
        this.transform.position = new Vector3(p.x, p.y);

        Debug.Log(p);
    }
}
