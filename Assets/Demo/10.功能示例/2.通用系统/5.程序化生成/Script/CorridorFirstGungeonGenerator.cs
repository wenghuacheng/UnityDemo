using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// ������������
    /// </summary>
    public class CorridorFirstGungeonGenerator : SimpleRandomWalkGungeonGenerator
    {
        //���ȳ��ȣ�����
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
            HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();//Ǳ�ڷ���λ��

            //��������
            CreateCorridors(floorPositions, potentialRoomPositions);

            //��������
            HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);        

            //��������ͬ�����Ⱦ�ͷû�з��䣩
            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
            CreateRoomsAtDeadEnd(deadEnds, roomPositions);

            floorPositions.UnionWith(roomPositions);

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        /// <summary>
        /// ������������
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
                currentPosition = corridor[corridor.Count - 1];//ʹ�����һ������Ϊһ�����ɵ���ʼ�㣬������Ҫlist������hashset

                potentialPositions.Add(currentPosition);//��¼Ǳ�ڿ����ɷ���λ��
                floorPositions.UnionWith(corridor);
            }
        }

        /// <summary>
        /// ���ɷ�������
        /// </summary>
        /// <param name="potentialPositions"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

            //�������õı��������ɷ��䣬���ʱ1��ÿ�����ȵľ�ͷ�������ɷ���
            int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            //ͨ����ÿ��Ԫ������һ����ʱ��guid�������������б�˳��
            List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(p => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(roomGenerationParameter, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }

            return roomPositions;
        }

        /// <summary>
        /// ������ͬҲ��������
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
        /// �ҵ�����ͬ
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

                //ֻ��һ��������������Ϊ����ͬ
                if (neighboursCount <= 1)
                    deadEnds.Add(position);
            }

            return deadEnds;
        }


    }
}