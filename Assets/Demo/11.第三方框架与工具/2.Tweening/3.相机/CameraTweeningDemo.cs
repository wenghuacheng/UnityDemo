using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweeningDemo : MonoBehaviour
{
    void Start()
    {
        var camera = Camera.main;

        //设置相机的宽高比
        //camera.DOAspect(1, 3);

        //【所有相机适用】设置摄像机的近切面和远切面（Clipping Plane）Near,Far
        //cam.DONearClipPlane(1.4f, 2f);
        //cam.DOFarClipPlane(5f, 2f);

        //【投影相机】视距
        //cam.DOFieldOfView(100f, 2f);

        //【正交摄像机】修改otherographic的视域大小
        //camera.DOOrthoSize(3f, 2f);

        //设置摄像机在屏幕的比例(数值为比例)，viewport属性
        //camera.DORect(new Rect(0, 0, 0.5f, 0.5f),2f);

        //设置摄像机在屏幕的比例（数值为像素）
        //camera.DOPixelRect(new Rect(0, 0, 512, 384), 2f);

        //设置相机的震动(float时长，float 强度,int 频率,float 随机度数)
        camera.DOShakePosition(2, 0.3f);
    }

}
