using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.PCG
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap;
        [SerializeField] private TileBase floorTile, wallTile;//地板/墙体Tile

        /// <summary>
        /// 绘制地板
        /// </summary>
        /// <param name="floorPositions"></param>
        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintTiles(floorPositions, floorTilemap, floorTile);
        }

        private void PaintTiles(IEnumerable<Vector2Int> floorPositions, Tilemap tilemap, TileBase tile)
        {
            foreach (var postion in floorPositions)
            {
                PaintSingleTile(tilemap, tile, postion);
            }
        }

        /// <summary>
        /// 绘制单个地板
        /// </summary>
        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        public void PaintSingleBasicWall(Vector2Int position)
        {
            PaintSingleTile(wallTilemap, wallTile, position);
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }
    }
}