using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// ������������
    /// </summary>
    public class RoomFirstDungeonGenerator : SimpleRandomWalkGungeonGenerator
    {
        [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;//��С����ߴ�
        [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;//���µ�ͼ�ߴ�

        [SerializeField]
        [Range(0, 10)]
        private int offest = 1;//����ƫ�ƣ��������ɵķ��䶼������һ���

        [SerializeField] private bool randomWalkRoom = false;//�����ڲ��Ƿ����

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            var roomList = ProveduaralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPostion,
                new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            if (randomWalkRoom)
            {
                floor = CreateRoomsRandomly(roomList);
            }
            else
            {
                floor = CreateSimpleRooms(roomList);
            }

            //�������ĵ�
            List<Vector2Int> roomCenters = new List<Vector2Int>();
            foreach (var room in roomList)
            {
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            }

            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);

            tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualizer);
        }

        /// <summary>
        /// ʹ�������㷨�������䣨�������ķ����ķ��䣩
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            for (int i = 0; i < roomList.Count; i++)
            {
                var roomBounds = roomList[i];
                var roomCenter = (Vector2Int)Vector3Int.RoundToInt(roomBounds.center);

                var roomFloor = RunRandomWalk(randomWalkParameter, roomCenter);
                foreach (var position in roomFloor)
                {
                    //ֻ�з��ϳߴ�Ĳ��������
                    if (position.x >= (roomBounds.xMin + offest) && position.x <= (roomBounds.xMax - offest)
                        && position.y >= (roomBounds.yMin + offest) && position.y <= roomBounds.yMax - offest)
                    {
                        floor.Add(position); 
                    }
                }
            }

            return floor;
        }

        /// <summary>
        /// ����������������꣨���ķ����ķ��䣩
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            foreach (var room in roomList)
            {
                //�����ɵķ��䷶Χ�����ƫ����
                for (int col = offest; col < room.size.x - offest; col++)
                {
                    for (int row = offest; row < room.size.y - offest; row++)
                    {
                        var position = (Vector2Int)room.min + new Vector2Int(col, row);
                        floor.Add(position);
                    }
                }
            }
            return floor;
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="roomCenters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            //���һ�����䣬�Դ˷���Ϊ��ʼ
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            //��ѯ�÷�������ķ������ģ�������������
            while (roomCenters.Count > 0)
            {
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

                //���µķ������ĵ���Ϊ��һ�����ӷ������ʼ��
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        /// <summary>
        /// ������������������֮�䴴������
        /// </summary>
        /// <param name="currentRoomCenter"></param>
        /// <param name="closest"></param>
        /// <returns></returns>
        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
            corridor.Add(currentRoomCenter);

            var position = currentRoomCenter;
            //Y������
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                    position += Vector2Int.up;
                else if (destination.y < position.y)
                    position += Vector2Int.down;
                corridor.Add(position);
            }
            //X������
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                    position += Vector2Int.right;
                else if (destination.x < position.x)
                    position += Vector2Int.left;
                corridor.Add(position);
            }

            return corridor;
        }

        /// <summary>
        /// ��ѯ�ﵱǰ�������������һ������
        /// </summary>
        /// <param name="currentRoomCenter"></param>
        /// <param name="roomCenters"></param>
        /// <returns></returns>
        private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;
            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }
            return closest;
        }
    }
}