using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraPositionRange : MonoBehaviour
    {
        void Start()
        {
            //这里将相机进行了偏移
            var camera = Camera.main;

            //(0，0)点为左下角点  (1,1)点为右上角点

            //左上角
            var leftTop = camera.ViewportToWorldPoint(new Vector3(0, 1));
            //中上
            var middleTop = camera.ViewportToWorldPoint(new Vector3(0.5f, 1));
            //右上角
            var rightTop = camera.ViewportToWorldPoint(new Vector3(1, 1));


            Debug.Log("上部：" + leftTop + "-" + middleTop + "-" + rightTop);

            var leftMiddle = camera.ViewportToWorldPoint(new Vector3(0, 0.5f));
            //右中
            var rightMiddle = camera.ViewportToWorldPoint(new Vector3(1, 0.5f));
            Debug.Log("中部：" + leftMiddle + "-" + rightMiddle);

            //左下角
            var leftBottom = camera.ViewportToWorldPoint(new Vector3(0, 0));
            //中下
            var middleBottom = camera.ViewportToWorldPoint(new Vector3(0.5f, 0));
            //右下角
            var rightBottom = camera.ViewportToWorldPoint(new Vector3(1, 0));

            Debug.Log("底部：" + leftBottom + "-" + middleBottom + "-" + rightBottom);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}