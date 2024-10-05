using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// ����ǽ��
    /// </summary>
    public class WallGenerator : MonoBehaviour
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            var basicWallPosition = FindWallInDirections(floorPositions, Direction2D.cardinalDirectionsList);
            foreach (var position in basicWallPosition)
            {
                tilemapVisualizer.PaintSingleBasicWall(position);
            }
        }

        /// <summary>
        /// ��ȡ��Ҫ����ǽ���λ��
        /// </summary>
        private static HashSet<Vector2Int> FindWallInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
            foreach (var position in floorPositions)
            {
                foreach (var direction in directionsList)
                {
                    var neighbourPosition = position + direction;
                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        wallPositions.Add(neighbourPosition);
                    }
                }
            }
            return wallPositions;
        }
    }
}