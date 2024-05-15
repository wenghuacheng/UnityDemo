using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MineSweep
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] private int row = 8;
        [SerializeField] private int col = 8;
        [SerializeField] private Cell cellPrefab;

        private Camera _camera;

        //����ߴ�
        private int gridSize = 10;
        //��������
        private int maxMineCount = 20;
        //�߼�����(0:������1������)
        private int[,] map;
        //UI����
        private Cell[,] cells;

        //��ͼ��ʼ��λ��
        private Vector3 mapStartPosition = Vector3.zero;

        #region ��ʼ��
        private void Start()
        {
            _camera = Camera.main;

            InitializeMap();
            InitializeVisual();
        }

        /// <summary>
        /// ��ʼ���߼���ͼ
        /// </summary>
        private void InitializeMap()
        {
            map = new int[row, col];
            //�������
            int mineCount = 0;
            while (mineCount < maxMineCount)
            {
                var rowIndex = Random.Range(0, row);
                var colIndex = Random.Range(0, col);

                if (map[rowIndex, colIndex] == 1)
                    continue;

                map[rowIndex, colIndex] = 1;
                mineCount++;
            }
        }

        /// <summary>
        /// ��ʼ����ʾ��ͼ
        /// </summary>
        private void InitializeVisual()
        {
            cells = new Cell[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Vector2 pos = GetGridPosition(i, j);
                    var cell = Instantiate(cellPrefab, pos, Quaternion.identity, this.transform);
                    //���õ�ǰ��Ԫ��Ϊ����
                    if (map[i, j] == 1)
                        cell.isMineCell = true;

                    cell.SetStatus(0);
                    cells[i, j] = cell;
                }
            }
        }
        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                CellClickHandler();
        }

        /// <summary>
        /// ��Ԫ����
        /// </summary>
        private void CellClickHandler()
        {
            var vec = GetClickGridIndex();
            if (!IsVaildCell(vec)) return;

            if (cells[vec.x, vec.y].isMineCell)
            {
                Debug.Log("�ȵ�������");
                var curUICell = cells[vec.x, vec.y];
                curUICell.SetStatus(1);
                return;
            }
            else if (cells[vec.x, vec.y].Status == 1)
            {
                return;
            }

            //��ʼ�����������������ܵĸ���
            SearchAroundCell(vec);
        }

        /// <summary>
        /// 8������ĵ�Ԫ����
        /// </summary>
        /// <param name="positon"></param>
        private void SearchAroundCell(Vector2Int position)
        {
            if (!IsVaildCell(position))
                return;

            var curUICell = cells[position.x, position.y];

            //��ǰ���Ϊ������
            curUICell.SetStatus(1);

            int mineCount = 0;
            List<Vector2Int> aroundPostion = new List<Vector2Int>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    var p = new Vector2Int(position.x + i, position.y + j);
                    if (!IsVaildCell(p))
                        continue;

                    var cell = cells[p.x, p.y];

                    //���׽ڵ�
                    if (cell.isMineCell)
                    {
                        mineCount++;
                    }

                    if (cell.Status == 0)
                    {
                        //δ�����ĸ���
                        aroundPostion.Add(p);
                    }
                }
            }

            //ˢ�µ�Ԫ��UI
            curUICell.SetMineCount(mineCount);

            //ֻ����Χû�е��ײ���չ
            if (mineCount <= 0)
            {
                foreach (var p in aroundPostion)
                {
                    SearchAroundCell(p);
                }
            }
        }

        #region ���񷽷�

        /// <summary>
        /// ��ȡ����λ��
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGridPosition(int rowIndex, int colIndex)
        {
            return mapStartPosition + new Vector3(rowIndex * gridSize, colIndex * gridSize);
        }

        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        /// <returns></returns>
        private Vector2Int GetClickGridIndex()
        {
            var clickPos = _camera.ScreenToWorldPoint(Input.mousePosition) - mapStartPosition;

            //�������ĵ����⣬��Ҫƫ�ư����Ԫ��
            int xIndex = (int)((clickPos.x + gridSize / 2) / gridSize);
            int yIndex = (int)((clickPos.y + gridSize / 2) / gridSize);

            if (xIndex > row - 1 || yIndex > col - 1 || xIndex < 0 || yIndex < 0)
                return new Vector2Int(-1, -1);
            else
                return new Vector2Int(xIndex, yIndex);
        }

        /// <summary>
        /// �Ƿ��ǺϷ��ĵ�Ԫ��
        /// </summary>
        /// <returns></returns>
        private bool IsVaildCell(Vector2 position)
        {
            if (position.x > row - 1 || position.y > col - 1 || position.x < 0 || position.y < 0)
                return false;
            else
                return true;
        }
        #endregion

    }
}