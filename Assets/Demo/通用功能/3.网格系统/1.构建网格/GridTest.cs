using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Common.Grids
{
    public class GridTest : MonoBehaviour
    {
        [SerializeField] private Camera mCamera;

        private Grid<int> grid1;
        private Grid<bool> grid2;

        void Start()
        {
            grid1 = new Grid<int>(4, 3, 10f, Vector3.zero);
            grid2 = new Grid<bool>(2, 2, 10f, new Vector3(-30, -30));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //注意：相机必须为投影
                var position = mCamera.ScreenToWorldPoint(Input.mousePosition);
                int value = grid1.GetCellValue(position);
                grid1.SetCellValue(position, value + 5);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                var position = mCamera.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(grid1.GetCellValue(position));
            }
        }

    }
}