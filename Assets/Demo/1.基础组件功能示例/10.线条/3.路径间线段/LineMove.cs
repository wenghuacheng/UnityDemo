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

        //线的长度
        private float distance = 3;
        //线移动速度
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

            //测试代码
            //middlePoints.Add(position[1]);
            //DrawLine(position[2], position[0], middlePoints);
        }

        /// <summary>
        /// 处理线段开始点
        /// </summary>
        void HandleStartPoint()
        {
            if (index >= position.Count)
            {
                //起始点已经到达位置
                return;
            }

            var target = position[index];

            lineStartPoint = Vector3.MoveTowards(lineStartPoint, target, Time.deltaTime * speed);
            if (Vector3.Distance(lineStartPoint, target) < 0.005f)
            {
                //到达目标点，将其作为拐点
                middlePoints.Add(target);
                index++;
            }
        }

        /// <summary>
        /// 处理线段结束点
        /// </summary>
        void HandleEndPoint()
        {
            //判断距离,如果距离小于目标距离则结束点不动.
            //除非起始点已经到达终点
            var currentDistance = PointsDistance(lineStartPoint, lineEndPoint, middlePoints);
            if (currentDistance < distance && index < position.Count)
            {
                return;
            }

            if (middlePoints.Count > 0)
            {
                //向拐点移动
                var middlePoint = middlePoints[0];
                lineEndPoint = Vector3.MoveTowards(lineEndPoint, middlePoint, Time.deltaTime * speed);
                if (Vector3.Distance(lineEndPoint, middlePoint) < 0.005f)
                {
                    middlePoints.RemoveAt(0);
                }
            }
            else
            {
                //向开始点移动
                lineEndPoint = Vector3.MoveTowards(lineEndPoint, lineStartPoint, Time.deltaTime * speed);
            }
        }


        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="points"></param>
        void DrawLine(Vector3 lineStartPoint, Vector3 lineEndPoint, List<Vector3> points)
        {
            List<Vector3> tempPoints = new List<Vector3>();
            tempPoints.Add(lineStartPoint);
            tempPoints.AddRange(points);
            tempPoints.Add(lineEndPoint);

            //好像不设置宽度结束时线会变细
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startWidth = 0.1f;

            //ps:这么绘制好像才不会有问题，否则回出现只绘制一段的情况
            lineRenderer.positionCount = tempPoints.Count;
            for (int i = 0; i < tempPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, tempPoints[i]);
            }
        }

        /// <summary>
        /// 计算首尾与中间拐点的距离总和
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