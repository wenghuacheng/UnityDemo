using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// 文本显示的网格
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TextGrid<T>
    {
        //起始点【网格不可能一致是(0，0)点起始，会有偏移】
        private Vector3 originPosition;
        //网格数组
        private T[,] gridArray;
        //网格文本控件数组
        private TextMesh[,] gridTextArray;
        //单元格(x,y)变更事件
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
                    //在每个单元格创建文本
                    //需要位移将文本在单元格居中
                    gridTextArray[x, y] = CreateWorldText(null, gridArray[x, y]?.ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f);

                    //画出表格
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
                }
            }

            //画出表格上方的封闭线
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
        }

        #endregion

        //宽高，单元格相关属性
        public int width { get; private set; }
        public int height { get; private set; }
        public float cellSize { get; private set; }

        /// <summary>
        /// 基于网格位置获取世界坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 GetWorldPosition(int x, int y)
        {
            //注意起始点
            return new Vector3(x, y) * cellSize + originPosition;
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                //设置显示控件中的值
                gridTextArray[x, y].text = value.ToString();
                //单元格事件变更
                OnGridValueChanged?.Invoke(x, y);
            }
        }

        /// <summary>
        /// 设置单元格的值
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
        /// 获取单元格的值
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
        /// 获取单元格的值
        /// </summary>
        /// <param name="worldPosition"></param>
        public T GetCellValue(Vector3 worldPosition)
        {
            var c = GetXY(worldPosition);
            return GetCellValue(c.Item1, c.Item2);
        }

        /// <summary>
        /// 获取所在的单元格
        /// </summary>
        /// <param name="postion"></param>
        /// <returns></returns>
        public Tuple<int, int> GetXY(Vector3 position)
        {
            //需要基于起始点偏移
            int x = (int)MathF.Floor((position - originPosition).x / cellSize);
            int y = (int)MathF.Floor((position - originPosition).y / cellSize);

            return new Tuple<int, int>(x, y);
        }


        /// <summary>
        /// 生成文本控件
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