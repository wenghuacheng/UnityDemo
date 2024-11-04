using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// λ������
    /// </summary>
    public class CircleItemPosition
    {
        public CircleItemPosition(int depth, int index)
        {
            this.Depth = depth;
            this.Index = index;
        }

        /// <summary>
        /// ���
        /// </summary>
        public int Depth { get; private set; }

        /// <summary>
        /// ˳������
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// ��˳��
        /// </summary>
        public int SiblingIndex { get; set; }

        /// <summary>
        /// λ��
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// ���ųߴ�
        /// </summary>
        public Vector3 Scale { get; private set; }

        /// ��ʼ������
        /// </summary>
        /// <param name="totalItemCount">�ܸ���</param>
        /// <param name="totalOffest">ȫƫ����</param>
        /// <param name="raduis">����</param>
        /// <param name="yInterval">������ȵ�Y��ƫ��</param>
        /// <param name="scaleInterval">������ȵ�����</param>
        public void InitData(int totalItemCount, Vector3 totalOffest, float radius = 100f, float yInterval = 100f, float scaleInterval = 0.08f)
        {
            //����X��Z������
            float angle = 360 / (float)totalItemCount;
            var x = Mathf.Sin(angle * Index * Mathf.Deg2Rad) * radius;
            //var z = Mathf.Cos(angle * Index * Mathf.Deg2Rad) * radius;//z��ûɶ�ã�ͨ��SetSiblingIndex

            //������ȵ�Y��ƫ����
            var yOffest = yInterval * Depth;

            //���ò�˳��
            InitSiblingIndex(totalItemCount);

            //����λ�������ű���
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
                //���
                SiblingIndex = totalItemCount - this.Depth * 2 - 1;
            }
            else
            {
                //�Ҳ�
                SiblingIndex = totalItemCount - this.Depth * 2;
            }

        }
    }
}