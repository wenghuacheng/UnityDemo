using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class BezierMove : MonoBehaviour
    {
        public Transform target;
        private Vector2 startPosition;
        private Vector2 middlePosition;

        private float percent = 0;

        void Start()
        {
            //�÷�������һ��bug����Ŀ�������ƶ�ʱ�ƶ���ǳ�����Ȼ������ֻ�����ڱȽϿ������

            startPosition = this.transform.position;
            middlePosition = GetMiddlePoint(startPosition, target.position);
            Debug.Log("startPosition" + startPosition);
            Debug.Log("middlePosition" + middlePosition);
            Debug.Log("target" + target.position);
        }

        // Update is called once per frame
        void Update()
        {
            percent += Time.deltaTime * 0.2f;
            if (percent > 1)
                percent = 1;

            //ͨ����ǰ�İٷֱȻ�ȡ�ڱ����������ϵ�λ��
            var p = Bezior(percent, startPosition, middlePosition, target.position);
            this.transform.position = p;

            //ͨ�����㷢��㵽�м�㣬�м�㵽Ŀ�������һ���ߣ������߾����ƶ��ķ�������
            var ab = Vector2.Lerp(startPosition, middlePosition, percent);
            var bc = Vector2.Lerp(middlePosition, target.position, percent);
            this.transform.up = (ab - bc).normalized;
        }

        /// <summary>
        /// ���㱴�������߲�ֵ
        /// </summary>
        /// <param name="t"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public Vector2 Bezior(float t, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = Vector2.Lerp(a, b, t);
            var bc = Vector2.Lerp(b, c, t);
            //����ʼ�㣬�м�㣬Ŀ�������������ͨ��������ȡһ��������
            //��������ǵ�ǰ��λ��
            return Vector2.Lerp(ab, bc, t);
        }

        /// <summary>
        /// �������ĵ�
        /// </summary>
        public Vector2 GetMiddlePoint(Vector2 a, Vector2 b)
        {
            var middle = Vector2.Lerp(a, b, 0.1f);

            //��ȡ��ʼ������Ŀ�������Ĵ�ֱ����
            var directVertical = Vector2.Perpendicular(a - b).normalized;
            //������ϻ�������
            var rd = Random.Range(-2, 2);
            var length = (a - b).magnitude * 0.3f;

            //ƽ���ı��η��򻭳��µ����ĵ�
            return middle + directVertical * length * rd;

        }
    }
}