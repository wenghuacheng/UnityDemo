using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraPositionRange : MonoBehaviour
    {
        void Start()
        {
            //���ｫ���������ƫ��
            var camera = Camera.main;

            //(0��0)��Ϊ���½ǵ�  (1,1)��Ϊ���Ͻǵ�

            //���Ͻ�
            var leftTop = camera.ViewportToWorldPoint(new Vector3(0, 1));
            //����
            var middleTop = camera.ViewportToWorldPoint(new Vector3(0.5f, 1));
            //���Ͻ�
            var rightTop = camera.ViewportToWorldPoint(new Vector3(1, 1));


            Debug.Log("�ϲ���" + leftTop + "-" + middleTop + "-" + rightTop);

            var leftMiddle = camera.ViewportToWorldPoint(new Vector3(0, 0.5f));
            //����
            var rightMiddle = camera.ViewportToWorldPoint(new Vector3(1, 0.5f));
            Debug.Log("�в���" + leftMiddle + "-" + rightMiddle);

            //���½�
            var leftBottom = camera.ViewportToWorldPoint(new Vector3(0, 0));
            //����
            var middleBottom = camera.ViewportToWorldPoint(new Vector3(0.5f, 0));
            //���½�
            var rightBottom = camera.ViewportToWorldPoint(new Vector3(1, 0));

            Debug.Log("�ײ���" + leftBottom + "-" + middleBottom + "-" + rightBottom);
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