using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// 通过三角函数计算旋转角度
    /// </summary>
    public class MathRotationPosition : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            //起始点
            var originPoint = new Vector3(3, 2);
            //线段长度
            var distance = 2;
            //这里以30度为一个扇区
            int count = 12;
            float angleIncrease = 360 / count;

            for (int i = 0; i < count; i++)
            {
                var angle = angleIncrease * i;
                var p = GetPosition(angle);

                //从起始点画一条线
                //通过向量加法，让旋转后的点以起始点为基准
                Gizmos.DrawLine(originPoint, originPoint + p * distance);
            }
        }

        /// <summary>
        /// 获取旋转角度后的点【逆时针】
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Vector3 GetPosition(float angle)
        {
            //将角度转换为弧度
            var radian = angle * Mathf.Deg2Rad;
            //通过三角函数计算
            //Y值：对边即sin，X值：临边即cos
            return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
        }
    }
}