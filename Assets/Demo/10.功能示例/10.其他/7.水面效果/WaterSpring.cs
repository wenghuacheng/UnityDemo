using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Demo.Other.Wave
{
    /// <summary>
    /// �õ���Ч��ģ�⺣��
    /// </summary>
    public class WaterSpring : MonoBehaviour
    {
        public float velocity = 0;
        public float force = 0;

        public float height = 0;//��ǰ�߶�
        public float targetHeight = 0;

        private SpriteShapeController ssc;
        private int waveIndex;


        /// <summary>
        /// ��ʼ��
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
        /// ���¿��Ƶ�����λ��
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

        #region �˶�����
        /// <summary>
        /// �����˶�
        /// </summary>
        /// <param name="springStiffness"></param>
        public void WaveSpringUpdate(float springStiffness)
        {
            height = transform.localPosition.y;

            //��ǰ�߶���Ŀ��߶ȵĲ�ֵ
            var x = height - targetHeight;

            //��Ҫ��FixedUpdate�����У�ÿ�ε�������Ŀ������ƶ���Խ���ƶ�����ԽС
            force = -springStiffness * x;
            //��Ҫ��¼֮ǰ�Ļ����������Ĵ�С����Ȼ�ᵼ�������ƶ�������Χ�����������ļ��ٲ������෴�����˶�
            velocity += force;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + velocity, transform.localPosition.z);
        }

        /// <summary>
        /// �����˶��������᡿
        /// </summary>
        /// <param name="springStiffness"></param>
        public void WaveSpringDampeningUpdate(float springStiffness, float dampening)
        {
            height = transform.localPosition.y;

            //��ǰ�߶���Ŀ��߶ȵĲ�ֵ
            var x = height - targetHeight;
            //ͨ���򷴷���ʩ��һ�����������ٶ�������С
            //���ٶ�Ϊ0ʱ����ҲΪ0����֤�������෴�����˶�
            var loss = -dampening * velocity;

            //��Ҫ��FixedUpdate�����У�ÿ�ε�������Ŀ������ƶ���Խ���ƶ�����ԽС
            force = -springStiffness * x + loss;
            //��Ҫ��¼֮ǰ�Ļ����������Ĵ�С����Ȼ�ᵼ�������ƶ�������Χ�����������ļ��ٲ������෴�����˶�
            velocity += force;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + velocity, transform.localPosition.z);
        }
        #endregion
    }
}