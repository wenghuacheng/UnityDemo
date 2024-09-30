using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    /// <summary>
    /// AStar数据数组
    /// </summary>
    [Serializable]
    public class AStarGridArray<T> where T : new()
    {
        [SerializeField] public T[,] gridDataArray;

        private int row, col;

        public AStarGridArray(int row, int col)
        {
            this.row = row;
            this.col = col;

            gridDataArray = new T[row, col];
        }

        public T GetData(int rowIndex, int colIndex)
        {
            try
            {
                if (rowIndex >= row || colIndex >= col) return default(T);
                return gridDataArray[rowIndex, colIndex];
            }
            catch (Exception)
            {
                Debug.Log($"出错:{rowIndex},{colIndex}");
                return default(T);
            }
          
        }

        public void SetData(int rowIndex, int colIndex, T data)
        {
            if (rowIndex >= row || colIndex >= col) return;
            gridDataArray[rowIndex, colIndex] = data;
        }
    }
}