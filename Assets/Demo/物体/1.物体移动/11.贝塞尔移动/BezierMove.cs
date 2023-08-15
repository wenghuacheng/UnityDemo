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
            //该方法存在一个bug就是目标物体移动时移动会非常不自然，所以只能用于比较快的物体

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

            //通过当前的百分比获取在贝塞尔曲线上的位置
            var p = Bezior(percent, startPosition, middlePosition, target.position);
            this.transform.position = p;

            //通过计算发射点到中间点，中间点到目标点上拉一条线，这条线就是移动的方向向量
            var ab = Vector2.Lerp(startPosition, middlePosition, percent);
            var bc = Vector2.Lerp(middlePosition, target.position, percent);
            this.transform.up = (ab - bc).normalized;
        }

        /// <summary>
        /// 计算贝塞尔曲线差值
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
            //在起始点，中间点，目标点这三个点上通过比例获取一个点坐标
            //该坐标就是当前的位置
            return Vector2.Lerp(ab, bc, t);
        }

        /// <summary>
        /// 计算中心点
        /// </summary>
        public Vector2 GetMiddlePoint(Vector2 a, Vector2 b)
        {
            var middle = Vector2.Lerp(a, b, 0.1f);

            //获取起始向量到目标向量的垂直向量
            var directVertical = Vector2.Perpendicular(a - b).normalized;
            //随机向上还是向下
            var rd = Random.Range(-2, 2);
            var length = (a - b).magnitude * 0.3f;

            //平行四边形法则画出新的中心点
            return middle + directVertical * length * rd;

        }
    }
}