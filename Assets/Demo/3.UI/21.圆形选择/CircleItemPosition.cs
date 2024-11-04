using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// 位置数据
    /// </summary>
    public class CircleItemPosition
    {
        public CircleItemPosition(int depth, int index)
        {
            this.Depth = depth;
            this.Index = index;
        }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; private set; }

        /// <summary>
        /// 顺序索引
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 层顺序
        /// </summary>
        public int SiblingIndex { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// 缩放尺寸
        /// </summary>
        public Vector3 Scale { get; private set; }

        /// 初始化数据
        /// </summary>
        /// <param name="totalItemCount">总个数</param>
        /// <param name="totalOffest">全偏移量</param>
        /// <param name="raduis">长度</param>
        /// <param name="yInterval">基于深度的Y轴偏移</param>
        /// <param name="scaleInterval">基于深度的缩放</param>
        public void InitData(int totalItemCount, Vector3 totalOffest, float radius = 100f, float yInterval = 100f, float scaleInterval = 0.08f)
        {
            //计算X，Z的坐标
            float angle = 360 / (float)totalItemCount;
            var x = Mathf.Sin(angle * Index * Mathf.Deg2Rad) * radius;
            //var z = Mathf.Cos(angle * Index * Mathf.Deg2Rad) * radius;//z轴没啥用，通过SetSiblingIndex

            //基于深度的Y轴偏移量
            var yOffest = yInterval * Depth;

            //设置层顺序
            InitSiblingIndex(totalItemCount);

            //生成位置与缩放比例
            this.Position = new Vector3(x, yOffest, Depth) + totalOffest;
            this.Scale = Vector3.one - Vector3.one * scaleInterval * Depth;
        }

        private void InitSiblingIndex(int totalItemCount)
        {
            if (Index == 0)
            {
                SiblingIndex = totalItemCount - 1;
                return;
            }

            if (Index > totalItemCount / 2)
            {
                //左侧
                SiblingIndex = totalItemCount - this.Depth * 2 - 1;
            }
            else
            {
                //右侧
                SiblingIndex = totalItemCount - this.Depth * 2;
            }

        }
    }
}