using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// 程序化地图生成算法
    /// </summary>
    public static class ProveduaralGenerationAlgorithms
    {
        #region 随机游走算法
        /// <summary>
        /// 随机游走算法
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="walkLength"></param>
        /// <returns></returns>
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();

            path.Add(startPosition);
            var prevPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                var newPosition = prevPosition + Direction2D.GetRamdomCardinalDirection();
                path.Add(newPosition);
                prevPosition = newPosition;
            }

            return path;
        }

        #endregion

        #region 随机走廊算法
        /// <summary>
        /// 随机走廊算法
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="corridorLength"></param>
        /// <returns></returns>
        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridors = new List<Vector2Int>();

            //向着一个方向进行延展
            var direction = Direction2D.GetRamdomCardinalDirection();
            var currentPosition = startPosition;
            corridors.Add(currentPosition);

            for (int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridors.Add(currentPosition);
            }

            return corridors;
        }
        #endregion

        #region 二叉空间划分算法
        /// <summary>
        /// 二叉空间划分算法
        /// 将一整块区域不停的二分为两个房间,直到到达最小的不可分割尺寸
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
            List<BoundsInt> roomsList = new List<BoundsInt>();

            roomsQueue.Enqueue(spaceToSplit);

            while (roomsQueue.Count > 0)
            {
                var room = roomsQueue.Dequeue();

                //符合最小尺寸的区域才能被分割
                if (room.size.y >= minHeight && room.size.x >= minWidth)
                {
                    //随机分割方式
                    if (Random.value <= 0.5f)
                    {
                        //优先水平切割
                        if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minWidth, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                    else
                    {
                        //优先垂直分割
                        if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, room);
                        }
                        else if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                }               
            }

            return roomsList;
        }

        ///// <summary>
        ///// 垂直水平分割房间
        ///// </summary>
        private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            //X轴分割点。不能沿着边缘分割，只要要留一个单位的间距
            var xSplit = Random.Range(1, room.size.x);//完全随机算法
            //var xSplit = Random.Range(minWidth, room.size.x - minWidth);

            //计算两个分割房间的区域参数
            BoundsInt leftRoom = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));//左侧房间的尺寸
            BoundsInt rightRoom = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));//右侧房间的尺寸

            roomsQueue.Enqueue(leftRoom);
            roomsQueue.Enqueue(rightRoom);
        }

        /// <summary>
        /// 水平分割房间
        /// </summary>
        private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);//完全随机算法
            //var ySplit = Random.Range(minHeight, room.size.y - minHeight);

            //计算两个分割房间的区域参数
            BoundsInt downRoom = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));//下方房间的尺寸
            BoundsInt upRoom = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));//上方房间的尺寸

            roomsQueue.Enqueue(downRoom);
            roomsQueue.Enqueue(upRoom);
        }

        #endregion
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1),//上
        new Vector2Int(1, 0),//右
        new Vector2Int(0, -1),//下
        new Vector2Int(-1, 0),//左
    };

    /// <summary>
    /// 获取随机方向
    /// </summary>
    /// <returns></returns>
    public static Vector2Int GetRamdomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}