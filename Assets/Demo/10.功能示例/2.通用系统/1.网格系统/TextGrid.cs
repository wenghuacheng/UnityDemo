using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// �ı���ʾ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TextGrid<T>
    {
        //��ʼ�㡾���񲻿���һ����(0��0)����ʼ������ƫ�ơ�
        private Vector3 originPosition;
        //��������
        private T[,] gridArray;
        //�����ı��ؼ�����
        private TextMesh[,] gridTextArray;
        //��Ԫ��(x,y)����¼�
        public event Action<int, int> OnGridValueChanged;

        #region Ctor
        public TextGrid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new T[width, height];
            gridTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //��ÿ����Ԫ�񴴽��ı�
                    //��Ҫλ�ƽ��ı��ڵ�Ԫ�����
                    gridTextArray[x, y] = CreateWorldText(null, gridArray[x, y]?.ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f);

                    //�������
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
                }
            }

            //��������Ϸ��ķ����
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
        }

        #endregion

        //��ߣ���Ԫ���������
        public int width { get; private set; }
        public int height { get; private set; }
        public float cellSize { get; private set; }

        /// <summary>
        /// ��������λ�û�ȡ��������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 GetWorldPosition(int x, int y)
        {
            //ע����ʼ��
            return new Vector3(x, y) * cellSize + originPosition;
        }

        /// <summary>
        /// ���õ�Ԫ���ֵ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                //������ʾ�ؼ��е�ֵ
                gridTextArray[x, y].text = value.ToString();
                //��Ԫ���¼����
                OnGridValueChanged?.Invoke(x, y);
            }
        }

        /// <summary>
        /// ���õ�Ԫ���ֵ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(Vector3 worldPosition, T value)
        {
            var c = GetXY(worldPosition);
            SetCellValue(c.Item1, c.Item2, value);
        }

        /// <summary>
        /// ��ȡ��Ԫ���ֵ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T GetCellValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
                return gridArray[x, y];
            else return default(T);
        }

        /// <summary>
        /// ��ȡ��Ԫ���ֵ
        /// </summary>
        /// <param name="worldPosition"></param>
        public T GetCellValue(Vector3 worldPosition)
        {
            var c = GetXY(worldPosition);
            return GetCellValue(c.Item1, c.Item2);
        }

        /// <summary>
        /// ��ȡ���ڵĵ�Ԫ��
        /// </summary>
        /// <param name="postion"></param>
        /// <returns></returns>
        public Tuple<int, int> GetXY(Vector3 position)
        {
            //��Ҫ������ʼ��ƫ��
            int x = (int)MathF.Floor((position - originPosition).x / cellSize);
            int y = (int)MathF.Floor((position - originPosition).y / cellSize);

            return new Tuple<int, int>(x, y);
        }


        /// <summary>
        /// �����ı��ؼ�
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="localPosition"></param>
        /// <returns></returns>
        private TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;

            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.text = text;
            textMesh.color = Color.white;
            textMesh.fontSize = 12;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = 99;
            return textMesh;
        }

    }
}