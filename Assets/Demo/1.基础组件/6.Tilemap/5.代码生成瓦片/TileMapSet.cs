using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Maps
{
    public class TileMapSet : MonoBehaviour
    {
        public Tilemap tilemap;
        public Tile tile;

        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                tilemap.SetTile(new Vector3Int(i, 0, 0), tile);
            }
        }

        void Update()
        {

        }
    }
}