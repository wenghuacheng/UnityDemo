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
        [SerializeField] private TileBase floorTile, wallTile;//�ذ�/ǽ��Tile

        /// <summary>
        /// ���Ƶذ�
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
        /// ���Ƶ����ذ�
        /// </summary>
        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        /// <summary>
        /// ����
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