using Demo.Games.XXL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    public class GridSpawner : MonoBehaviour
    {
        [SerializeField] private GridMap map;
        [SerializeField] private GameObject gridPrefab;

        private List<GridMovableCellUI> renderCell = new List<GridMovableCellUI>();

        private void Awake()
        {
            map.OnCellEliminate += CellEliminateHandler;
        }

        void Start()
        {
        }

        void Update()
        {

        }

        #region Event Handler
        /// <summary>
        /// ��Ԫ������ʱ�䴦��
        /// </summary>
        private void CellEliminateHandler()
        {
            renderCell.Clear();

            var grid = map.Grid;
            for (int i = 0; i < map.Row; i++)
            {
                for (int j = 0; j < map.Col; j++)
                {
                    var cell = grid[i, j];
                    if (cell.type <= 0)
                    {
                        break;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// �������ĵ�Ԫ���Ƿ񵽵�
        /// </summary>
        private void CheckDropableToBelowCell()
        {

        }

        /// <summary>
        /// ������Ҫ�½��ĵ�Ԫ��
        /// </summary>
        private void GenerateDropableCell()
        {
            var grid = map.Grid;
            for (int i = 1; i < map.Row; i++)
            {
                for (int j = 1; j < map.Col; j++)
                {
                    var cell = grid[i, j];
                    if (cell.type > 0)
                    {
                        //����ĵ�Ԫ������ǿյģ������ɽڵ������׹
                        var belowCell = grid[i, j - 1];
                        if (belowCell.type <= 0)
                        {
                            var obj = Instantiate(gridPrefab, cell.center, Quaternion.identity);
                            var cellUI = obj.GetComponent<GridMovableCellUI>();
                            cellUI.SetTarget(belowCell.center);
                            renderCell.Add(cellUI);
                        }
                    }
                }
            }
        }
    }
}