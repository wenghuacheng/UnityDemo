using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// ��תӰ��ǰ������
    /// </summary>
    public class TransformRotationAndTranlateDemo : MonoBehaviour
    {
        void Update()
        {
            //��תʱ���������ϵҲ����Ӧ��ת
            this.transform.Rotate(0, 0, 45 * Time.deltaTime);
            //����������Ϸ����ƶ�
            this.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
}