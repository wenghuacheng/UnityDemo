using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Demo.Other.Wave
{
    /// <summary>
    /// 用弹簧效果模拟海浪
    /// </summary>
    public class WaterSpring : MonoBehaviour
    {
        public float velocity = 0;
        public float force = 0;

        public float height = 0;//当前高度
        public float targetHeight = 0;

        private SpriteShapeController ssc;
        private int waveIndex;


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ssc"></param>
        /// <param name="waveIndex"></param>
        public void Init(SpriteShapeController ssc, int waveIndex)
        {
            this.ssc = ssc;

            this.waveIndex = waveIndex;

            velocity = 0;
            height = transform.localPosition.y;
            targetHeight = transform.localPosition.y;
        }

        /// <summary>
        /// 更新控制点所在位置
        /// </summary>
        public void WavePointUpdate()
        {
            if (ssc != null)
            {
                Spline spline = ssc.spline;
                Vector3 wavePosition = spline.GetPosition(waveIndex) ;
                spline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
            }
        }

        #region 运动函数
        /// <summary>
        /// 弹簧运动
        /// </summary>
        /// <param name="springStiffness"></param>
        public void WaveSpringUpdate(float springStiffness)
        {
            height = transform.localPosition.y;

            //当前高度与目标高度的差值
            var x = height - targetHeight;

            //需要在FixedUpdate中运行，每次调用向着目标进行移动，越近移动幅度越小
            force = -springStiffness * x;
            //需要记录之前的还保留的力的大小，虽然会导致物体移动超过范围，但会慢慢的减少并向着相反方向运动
            velocity += force;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + velocity, transform.localPosition.z);
        }

        /// <summary>
        /// 弹簧运动【带阻尼】
        /// </summary>
        /// <param name="springStiffness"></param>
        public void WaveSpringDampeningUpdate(float springStiffness, float dampening)
        {
            height = transform.localPosition.y;

            //当前高度与目标高度的差值
            var x = height - targetHeight;
            //通过向反方向施加一个力，让其速度慢慢变小
            //当速度为0时阻尼也为0，保证不会向相反方向运动
            var loss = -dampening * velocity;

            //需要在FixedUpdate中运行，每次调用向着目标进行移动，越近移动幅度越小
            force = -springStiffness * x + loss;
            //需要记录之前的还保留的力的大小，虽然会导致物体移动超过范围，但会慢慢的减少并向着相反方向运动
            velocity += force;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + velocity, transform.localPosition.z);
        }
        #endregion
    }
}