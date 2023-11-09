using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids
{
    public class HeatMapGridTest : MonoBehaviour
    {
        [SerializeField] private Camera mCamera;
        [SerializeField] private HeatMapVisual heatMapVisual;

        private Grid<int> grid;

        void Start()
        {
            grid = new Grid<int>(20, 10, 10f, Vector3.zero);
            heatMapVisual.SetGird(grid);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //注意：相机必须为投影
                var position = mCamera.ScreenToWorldPoint(Input.mousePosition);
                int value = grid.GetCellValue(position);
                grid.SetCellValue(position, value + 5);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                var position = mCamera.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(grid.GetCellValue(position));
            }
        }

    }
}