using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweeningDemo : MonoBehaviour
{
    void Start()
    {
        var camera = Camera.main;

        //��������Ŀ�߱�
        //camera.DOAspect(1, 3);

        //������������á�����������Ľ������Զ���棨Clipping Plane��Near,Far
        //cam.DONearClipPlane(1.4f, 2f);
        //cam.DOFarClipPlane(5f, 2f);

        //��ͶӰ������Ӿ�
        //cam.DOFieldOfView(100f, 2f);

        //��������������޸�otherographic�������С
        //camera.DOOrthoSize(3f, 2f);

        //�������������Ļ�ı���(��ֵΪ����)��viewport����
        //camera.DORect(new Rect(0, 0, 0.5f, 0.5f),2f);

        //�������������Ļ�ı�������ֵΪ���أ�
        //camera.DOPixelRect(new Rect(0, 0, 512, 384), 2f);

        //�����������(floatʱ����float ǿ��,int Ƶ��,float �������)
        camera.DOShakePosition(2, 0.3f);
    }

}
