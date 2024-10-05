using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// ���򻯵�ͼ�����㷨
    /// </summary>
    public static class ProveduaralGenerationAlgorithms
    {
        #region ��������㷨
        /// <summary>
        /// ��������㷨
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

        #region ��������㷨
        /// <summary>
        /// ��������㷨
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="corridorLength"></param>
        /// <returns></returns>
        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridors = new List<Vector2Int>();

            //����һ�����������չ
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

        #region ����ռ仮���㷨
        /// <summary>
        /// ����ռ仮���㷨
        /// ��һ��������ͣ�Ķ���Ϊ��������,ֱ��������С�Ĳ��ɷָ�ߴ�
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

                //������С�ߴ��������ܱ��ָ�
                if (room.size.y >= minHeight && room.size.x >= minWidth)
                {
                    //����ָʽ
                    if (Random.value <= 0.5f)
                    {
                        //����ˮƽ�и�
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
                        //���ȴ�ֱ�ָ�
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
        ///// ��ֱˮƽ�ָ��
        ///// </summary>
        private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            //X��ָ�㡣�������ű�Ե�ָֻҪҪ��һ����λ�ļ��
            var xSplit = Random.Range(1, room.size.x);//��ȫ����㷨
            //var xSplit = Random.Range(minWidth, room.size.x - minWidth);

            //���������ָ����������
            BoundsInt leftRoom = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));//��෿��ĳߴ�
            BoundsInt rightRoom = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));//�Ҳ෿��ĳߴ�

            roomsQueue.Enqueue(leftRoom);
            roomsQueue.Enqueue(rightRoom);
        }

        /// <summary>
        /// ˮƽ�ָ��
        /// </summary>
        private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);//��ȫ����㷨
            //var ySplit = Random.Range(minHeight, room.size.y - minHeight);

            //���������ָ����������
            BoundsInt downRoom = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));//�·�����ĳߴ�
            BoundsInt upRoom = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));//�Ϸ�����ĳߴ�

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
        new Vector2Int(0, 1),//��
        new Vector2Int(1, 0),//��
        new Vector2Int(0, -1),//��
        new Vector2Int(-1, 0),//��
    };

    /// <summary>
    /// ��ȡ�������
    /// </summary>
    /// <returns></returns>
    public static Vector2Int GetRamdomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}