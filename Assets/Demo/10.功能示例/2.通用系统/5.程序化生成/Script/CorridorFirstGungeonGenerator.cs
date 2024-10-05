using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// 走廊优先生成
    /// </summary>
    public class CorridorFirstGungeonGenerator : SimpleRandomWalkGungeonGenerator
    {
        //走廊长度，数量
        [SerializeField] private int corridorLength = 14, corridorCount = 5;

        [SerializeField]
        [Range(0.1f, 1)]
        public float roomPercent = 0.8f;

        [SerializeField] SimpleRandomWalkParamSO roomGenerationParameter;

        protected override void RunProceduralGeneration()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();//潜在房间位置

            //创建走廊
            CreateCorridors(floorPositions, potentialRoomPositions);

            //创建房间
            HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);        

            //处理死胡同（走廊尽头没有房间）
            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
            CreateRoomsAtDeadEnd(deadEnds, roomPositions);

            floorPositions.UnionWith(roomPositions);

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        /// <summary>
        /// 生成走廊坐标
        /// </summary>
        /// <param name="floorPositions"></param>
        /// <param name="potentialPositions"></param>
        private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialPositions)
        {
            var currentPosition = startPostion;
            potentialPositions.Add(currentPosition);

            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = ProveduaralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[corridor.Count - 1];//使用最后一个点作为一次生成的起始点，所以需要list而不是hashset

                potentialPositions.Add(currentPosition);//记录潜在可生成房间位置
                floorPositions.UnionWith(corridor);
            }
        }

        /// <summary>
        /// 生成房间坐标
        /// </summary>
        /// <param name="potentialPositions"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

            //基于设置的比例来生成房间，如果时1则每个走廊的尽头都会生成房间
            int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            //通过给每个元素设置一个临时的guid并排序来打乱列表顺序
            List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(p => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(roomGenerationParameter, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }

            return roomPositions;
        }

        /// <summary>
        /// 给死胡同也创建房间
        /// </summary>
        /// <param name="deadEnds"></param>
        /// <param name="roomFloors"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if (!roomFloors.Contains(position))
                {
                    var roomFloor = RunRandomWalk(roomGenerationParameter, position);
                    roomFloors.UnionWith(roomFloor);
                }
            }
        }


        /// <summary>
        /// 找到死胡同
        /// </summary>
        /// <param name="floorPositions"></param>
        /// <returns></returns>
        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            List<Vector2Int> deadEnds = new List<Vector2Int>();
            foreach (var position in floorPositions)
            {
                int neighboursCount = 0;
                foreach (var direction in Direction2D.cardinalDirectionsList)
                {
                    if (floorPositions.Contains(position + direction))
                        neighboursCount++;
                }

                //只有一个方向有连接则为死胡同
                if (neighboursCount <= 1)
                    deadEnds.Add(position);
            }

            return deadEnds;
        }


    }
}