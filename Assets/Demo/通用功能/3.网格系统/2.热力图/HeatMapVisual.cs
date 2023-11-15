using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    public class HeatMapVisual : MonoBehaviour
    {
        private TextGrid<int> grid;
        private Mesh mesh;

        private void Awake()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void SetGird(TextGrid<int> grid)
        {
            this.grid = grid;
            UpdateHeatMapVisual();

            this.grid.OnGridValueChanged += Grid_OnGridValueChanged;
        }

        private void Grid_OnGridValueChanged(int arg1, int arg2)
        {
            UpdateHeatMapVisual();
        }

        /// <summary>
        /// 刷新Mesh
        /// </summary>
        private void UpdateHeatMapVisual()
        {
            MeshUtils.CreateEmptyMeshArrays(this.grid.width * this.grid.height, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

            for (int x = 0; x < this.grid.width; x++)
            {
                for (int y = 0; y < this.grid.height; y++)
                {
                    //获取网格的索引
                    int index = x * grid.height + y;
                    Debug.Log(index);

                    var quadSize = new Vector3(1, 1) * grid.cellSize;

                    int gridValue = grid.GetCellValue(x, y);
                    float gridValueNormalized = (float)gridValue / 100;
                    Vector2 gridValueUV = new Vector2(gridValueNormalized, 0);
                    MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);

                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }
    }
}