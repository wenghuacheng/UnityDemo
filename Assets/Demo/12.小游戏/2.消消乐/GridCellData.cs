using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    public class GridCellData
    {
        public GridCellData(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public int type;

        public GameObject prefab;

        public int x;

        public int y;

        //单元格中心点
        public Vector2 center;

        //范围
        public Rect rect;

        public bool Contains(Vector2 clickPosition)
        {
            return rect.Contains(clickPosition);
        }
    }
}