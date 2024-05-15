using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class PathCurveMove : MonoBehaviour
    {
        public GameObject WayList;
        private List<Vector3> pointList = new List<Vector3>();
        //����
        private float percent = 0;

        private void Awake()
        {
            for (int i = 0; i < WayList.transform.childCount; i++)
            {
                pointList.Add(WayList.transform.GetChild(i).position);
            }
        }

        void Start()
        {
            this.transform.position = pointList[0];
        }


        void Update()
        {
            if (percent > 1)
            {
                percent = 1;
                Debug.Log("����");
            }


            ////��������
            //stage=2;
            //var b = Bezier(pointList[0], pointList[1], pointList[2], percent);
            //this.transform.position = b.Item1;
            //this.transform.up = (b.Item3 - b.Item2).normalized;

            ////��������
            //stage=3;
            //var b = Bezier(pointList[0], pointList[1], pointList[2], pointList[3], percent);
            //this.transform.position = b.Item1;
            //this.transform.up = (b.Item3 - b.Item2).normalized;

            //N������
            //stage=4;
            var b = Bezier(percent, new List<Vector3>() { pointList[0], pointList[1], pointList[2], pointList[3], pointList[4], pointList[5] });
            this.transform.position = b.Item1;
            this.transform.up = (b.Item3 - b.Item2).normalized;

            percent += Time.deltaTime * 0.05f;
        }



        //һ�ױ��������ߣ�����P0��P1��t��Ӧ�Ϸ�ԭ���ڵ�һ�����߲���.
        Tuple<Vector3, Vector3, Vector3> Bezier(Vector3 p0, Vector3 p1, float t)
        {
            var temp = (1 - t) * p0 + t * p1;
            return new Tuple<Vector3, Vector3, Vector3>(temp, p0, p1);
        }

        //���ױ��������ߣ�������Ӧ�Ϸ�ԭ���ڵĶ������߲���.
        //����ֵ���ƣ���һ��Ϊ����㣬�ڶ������ֱ�Ϊ�����Ƶ����յ���Ƶ�
        Tuple<Vector3, Vector3, Vector3> Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 temp = (1 - t) * p0p1 + t * p1p2;
            return new Tuple<Vector3, Vector3, Vector3>(temp, p0p1, p1p2);
        }

        //���ױ��������ߣ�������Ӧ�Ϸ�ԭ���ڵ��������߲���.
        //����ֵ���ƣ���һ��Ϊ����㣬�ڶ������ֱ�Ϊ�����Ƶ����յ���Ƶ�
        Tuple<Vector3, Vector3, Vector3> Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 temp;
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 p2p3 = (1 - t) * p2 + t * p3;
            Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
            Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;
            temp = (1 - t) * p0p1p2 + t * p1p2p3;
            return new Tuple<Vector3, Vector3, Vector3>(temp, p0p1p2, p1p2p3);
        }

        // ��ױ��������ߣ�ʹ�õݹ�ʵ��.
        public Tuple<Vector3, Vector3, Vector3> Bezier(float t, List<Vector3> p)
        {
            if (p.Count <= 2)
                return Bezier(p[0], p[1], t);
            List<Vector3> newp = new List<Vector3>();
            for (int i = 0; i < p.Count - 1; i++)
            {
                Debug.DrawLine(p[i], p[i + 1]);

                Vector3 p0p1 = (1 - t) * p[i] + t * p[i + 1];
                newp.Add(p0p1);
            }
            return Bezier(t, newp);
        }
    }
}