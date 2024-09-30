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
                // �����λ�ô���Ļ����ת��Ϊ��������
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // ����z������2D��Ϸ�в���Ҫ�����ǽ�������Ϊ0
                mouseWorldPos.z = 0;

                // ����������ת��ΪTilemap�ϵĸ�������
                Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

                // ��ȡ��λ�õ���Ƭ
                TileBase clickedTile = tilemap.GetTile(cellPosition);

                // ���ȷʵ���������Ƭ
                if (clickedTile != null)
                {
                    Debug.Log("Clicked on tile at position: " + cellPosition);
                }
            }
        }
    }
}