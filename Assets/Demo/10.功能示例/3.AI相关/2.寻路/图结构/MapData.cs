using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.PathFind.GraphWay
{
    /// <summary>
    /// 地图信息
    /// </summary>
    public class MapData : MonoBehaviour
    {
        public int width = 10;
        public int height = 5;

        // Start is called before the first frame update
        void Start()
        {
            int[,] map = InitializeMap();
        }


        /// <summary>
        /// 初始化地图
        /// </summary>
        public int[,] InitializeMap()
        {
            int[,] map = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = 0;
                }

            }

            //生成障碍物墙体
            map[1, 0] = 1;
            map[1, 1] = 1;
            map[1, 2] = 1;
            map[3, 2] = 1;
            map[3, 3] = 1;
            map[3, 4] = 1;
            map[4, 2] = 1;
            map[5, 1] = 1;
            map[5, 2] = 1;
            map[6, 2] = 1;
            map[6, 3] = 1;
            map[8, 1] = 1;
            map[8, 2] = 1;
            map[8, 3] = 1;
            map[8, 4] = 1;

            return map;
        }
    }
}