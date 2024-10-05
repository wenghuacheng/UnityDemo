using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// 房间优先生成
    /// </summary>
    public class RoomFirstDungeonGenerator : SimpleRandomWalkGungeonGenerator
    {
        [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;//最小房间尺寸
        [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;//最下地图尺寸

        [SerializeField]
        [Range(0, 10)]
        private int offest = 1;//房间偏移，否则生成的房间都是贴在一起的

        [SerializeField] private bool randomWalkRoom = false;//房间内部是否随机

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

            //房间中心点
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
        /// 使用漫游算法创建房间（不是四四方方的房间）
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
                    //只有符合尺寸的才允许加入
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
        /// 创建单个房间的坐标（四四方方的房间）
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            foreach (var room in roomList)
            {
                //在生成的房间范围内添加偏移量
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
        /// 创建房间走廊
        /// </summary>
        /// <param name="roomCenters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            //随机一个房间，以此房间为开始
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            //查询该房间最近的房间中心，将其连接起来
            while (roomCenters.Count > 0)
            {
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

                //将新的房间中心点作为下一次连接房间的起始点
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        /// <summary>
        /// 在两个房间中心坐标之间创建走廊
        /// </summary>
        /// <param name="currentRoomCenter"></param>
        /// <param name="closest"></param>
        /// <returns></returns>
        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
            corridor.Add(currentRoomCenter);

            var position = currentRoomCenter;
            //Y轴走廊
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                    position += Vector2Int.up;
                else if (destination.y < position.y)
                    position += Vector2Int.down;
                corridor.Add(position);
            }
            //X轴走廊
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
        /// 查询里当前房间中心最近的一个房间
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