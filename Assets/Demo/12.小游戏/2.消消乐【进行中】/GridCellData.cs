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

        //��Ԫ�����ĵ�
        public Vector2 center;

        //��Χ
        public Rect rect;

        public bool Contains(Vector2 clickPosition)
        {
            return rect.Contains(clickPosition);
        }
    }
}