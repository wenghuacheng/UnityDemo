using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class LineMove : MonoBehaviour
    {
        public GameObject WayPoint;

        private LineRenderer lineRenderer;
        private List<Vector3> position = new List<Vector3>();
        private int index = 0;

        private Vector3 lineStartPoint;
        private Vector3 lineEndPoint;
        private List<Vector3> middlePoints = new List<Vector3>();

        //�ߵĳ���
        private float distance = 3;
        //���ƶ��ٶ�
        private int speed = 10;

        void Start()
        {
            for (int i = 0; i < WayPoint.transform.childCount; i++)
            {
                position.Add(WayPoint.transform.GetChild(i).position);
            }

            lineRenderer = GetComponent<LineRenderer>();

            lineStartPoint = position[index];
            lineEndPoint = position[index];
        }


        void Update()
        {
            HandleStartPoint();
            HandleEndPoint();
            DrawLine(lineStartPoint, lineEndPoint, middlePoints);

            //���Դ���
            //middlePoints.Add(position[1]);
            //DrawLine(position[2], position[0], middlePoints);
        }

        /// <summary>
        /// �����߶ο�ʼ��
        /// </summary>
        void HandleStartPoint()
        {
            if (index >= position.Count)
            {
                //��ʼ���Ѿ�����λ��
                return;
            }

            var target = position[index];

            lineStartPoint = Vector3.MoveTowards(lineStartPoint, target, Time.deltaTime * speed);
            if (Vector3.Distance(lineStartPoint, target) < 0.005f)
            {
                //����Ŀ��㣬������Ϊ�յ�
                middlePoints.Add(target);
                index++;
            }
        }

        /// <summary>
        /// �����߶ν�����
        /// </summary>
        void HandleEndPoint()
        {
            //�жϾ���,�������С��Ŀ�����������㲻��.
            //������ʼ���Ѿ������յ�
            var currentDistance = PointsDistance(lineStartPoint, lineEndPoint, middlePoints);
            if (currentDistance < distance && index < position.Count)
            {
                return;
            }

            if (middlePoints.Count > 0)
            {
                //��յ��ƶ�
                var middlePoint = middlePoints[0];
                lineEndPoint = Vector3.MoveTowards(lineEndPoint, middlePoint, Time.deltaTime * speed);
                if (Vector3.Distance(lineEndPoint, middlePoint) < 0.005f)
                {
                    middlePoints.RemoveAt(0);
                }
            }
            else
            {
                //��ʼ���ƶ�
                lineEndPoint = Vector3.MoveTowards(lineEndPoint, lineStartPoint, Time.deltaTime * speed);
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="points"></param>
        void DrawLine(Vector3 lineStartPoint, Vector3 lineEndPoint, List<Vector3> points)
        {
            List<Vector3> tempPoints = new List<Vector3>();
            tempPoints.Add(lineStartPoint);
            tempPoints.AddRange(points);
            tempPoints.Add(lineEndPoint);

            //�������ÿ�Ƚ���ʱ�߻��ϸ
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startWidth = 0.1f;

            //ps:��ô���ƺ���Ų��������⣬����س���ֻ����һ�ε����
            lineRenderer.positionCount = tempPoints.Count;
            for (int i = 0; i < tempPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, tempPoints[i]);
            }
        }

        /// <summary>
        /// ������β���м�յ�ľ����ܺ�
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        float PointsDistance(Vector3 lineStartPoint, Vector3 lineEndPoint, List<Vector3> points)
        {
            float distance = 0;

            List<Vector3> tempPoints = new List<Vector3>();
            tempPoints.Add(lineStartPoint);
            tempPoints.AddRange(points);
            tempPoints.Add(lineEndPoint);

            for (int i = 1; i < tempPoints.Count; i++)
            {
                distance += Vector3.Distance(tempPoints[i - 1], tempPoints[i]);
            }

            return distance;
        }
    }
}