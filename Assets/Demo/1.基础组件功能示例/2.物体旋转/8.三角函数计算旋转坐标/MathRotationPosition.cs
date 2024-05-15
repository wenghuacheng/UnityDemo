using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// ͨ�����Ǻ���������ת�Ƕ�
    /// </summary>
    public class MathRotationPosition : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            //��ʼ��
            var originPoint = new Vector3(3, 2);
            //�߶γ���
            var distance = 2;
            //������30��Ϊһ������
            int count = 12;
            float angleIncrease = 360 / count;

            for (int i = 0; i < count; i++)
            {
                var angle = angleIncrease * i;
                var p = GetPosition(angle);

                //����ʼ�㻭һ����
                //ͨ�������ӷ�������ת��ĵ�����ʼ��Ϊ��׼
                Gizmos.DrawLine(originPoint, originPoint + p * distance);
            }
        }

        /// <summary>
        /// ��ȡ��ת�ǶȺ�ĵ㡾��ʱ�롿
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Vector3 GetPosition(float angle)
        {
            //���Ƕ�ת��Ϊ����
            var radian = angle * Mathf.Deg2Rad;
            //ͨ�����Ǻ�������
            //Yֵ���Ա߼�sin��Xֵ���ٱ߼�cos
            return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
        }
    }
}