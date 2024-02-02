using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TempScript : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private AStarGridArray array;

    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        //Debug.Log($"{bounds.size.x},{bounds.size.y}");
        Debug.Log($"{bounds.xMin}|{bounds.xMax},{bounds.yMin}|{bounds.yMax}");

        array = new AStarGridArray(bounds.size.x, bounds.size.y, bounds.xMin, bounds.yMin);
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, 0));
                if (tilemap.HasTile(localPlace))
                {
                    array[localPlace.x, localPlace.y] = $"({localPlace.x},{localPlace.y})";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = tilemap.WorldToCell(world);
            Debug.Log($"pos:{pos},array:{array[pos.x, pos.y]}");
        }
    }

    public class AStarGridArray
    {
        public string[,] gridDataArray;
        private int xOffset;
        private int yOffset;

        public AStarGridArray(int row, int col, int xOffset, int yOffset)
        {
            gridDataArray = new string[row, col];
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public string this[int x, int y]
        {
            get { return gridDataArray[x - xOffset, y - yOffset]; }
            set { gridDataArray[x - xOffset, y - yOffset] = value; }
        }
    }
}
