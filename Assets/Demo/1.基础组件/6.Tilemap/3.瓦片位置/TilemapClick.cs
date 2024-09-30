using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Maps
{
    public class TilemapClick : MonoBehaviour
    {
        public Tilemap tilemap;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 将鼠标位置从屏幕坐标转换为世界坐标
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 由于z坐标在2D游戏中不重要，我们将其设置为0
                mouseWorldPos.z = 0;

                // 将世界坐标转换为Tilemap上的格子坐标
                Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                // 获取该位置的瓦片
                TileBase clickedTile = tilemap.GetTile(cellPosition);

                // 如果确实点击到了瓦片
                if (clickedTile != null)
                {
                    Debug.Log("Clicked on tile at position: " + cellPosition);
                }
            }
        }
    }
}