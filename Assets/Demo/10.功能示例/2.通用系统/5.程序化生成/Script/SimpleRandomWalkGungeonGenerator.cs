using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// 随机游走算法生成地图
    /// </summary>
    public class SimpleRandomWalkGungeonGenerator : AbstractDungeonGenerator
    {
        [SerializeField]protected SimpleRandomWalkParamSO randomWalkParameter;

        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameter, startPostion);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        /// <summary>
        /// 基于随机游走算法生成地板坐标
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkParamSO parameter, Vector2Int position)
        {
            var currentPosition = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < parameter.iterationts; i++)
            {
                var path = ProveduaralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameter.walkLength);
                floorPositions.UnionWith(path);

                //随机从已生成位置作为下一次生成的起始点
                if (parameter.startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }
    }
}