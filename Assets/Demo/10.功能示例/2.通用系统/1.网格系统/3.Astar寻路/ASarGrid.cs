using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Common.Grids
{
    public class ASarGrid : BaseRenderGrid
    {
        [SerializeField] private GameObject gridPrefab;
        private AStarGridArray<AStarGridCellData> gridDataArray;
        private AStarGridArray<AStarGridItemUI> gridUIArray;

        //��ʼ��Ԫ��
        private AStarGridCellData startCell;
        //������Ԫ��
        private AStarGridCellData endCell;

        #region ��ʼ��
        protected override void Awake()
        {
            gridDataArray = new AStarGridArray<AStarGridCellData>(row, col);
            InitializeCellData();
            gridUIArray = new AStarGridArray<AStarGridItemUI>(row, col);
            InitializeCellUIItem();
            //��ʼ���ϰ��ﵥԪ
            InitializeCellBlock();
        }

        /// <summary>
        /// ��ʼ����Ԫ������
        /// </summary>
        public void InitializeCellData()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    AStarGridCellData data = new AStarGridCellData() { position = new Vector2Int(i, j) };
                    gridDataArray.SetData(i, j, data);
                }
            }
        }

        /// <summary>
        /// ��ʼ����Ԫ��UI��Ŀ
        /// </summary>
        private void InitializeCellUIItem()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Vector2 position = new Vector2(i * gridWidth + gridWidth / 2, j * gridWidth + gridWidth / 2);
                    var obj = Instantiate(gridPrefab, position, Quaternion.identity, this.transform);
                    var itemUI = obj.GetComponent<AStarGridItemUI>();
                    var cellData = gridDataArray.GetData(i, j);
                    itemUI.Data = cellData;
                    gridUIArray.SetData(i, j, itemUI);
                }
            }
        }

        /// <summary>
        /// ��ʼ���ϰ��ﵥԪ
        /// </summary>
        private void InitializeCellBlock()
        {
            //��ǰֱ��д����
            gridDataArray.GetData(2, 0).type = 3;
            gridDataArray.GetData(2, 1).type = 3;
            gridDataArray.GetData(2, 2).type = 3;
            gridDataArray.GetData(2, 3).type = 3;

            RefreshAllCellUI();
        }
        #endregion

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //������Ԫ�����¼�����
                var offest = Vector3.zero;//��Ԫ��ƫ��ʱ����ʼλ��
                var cellPosition = GetMouseClickCellPosition(offest);
                if (!IsValidCellPosition(cellPosition)) return;
                CellClickHandler(cellPosition);
            }
        }

        #region Event Handler
        /// <summary>
        /// Ԫ�ص��
        /// </summary>
        /// <param name="obj"></param>
        protected override void CellClickHandler(Vector2Int position)
        {
            var data = gridUIArray.GetData(position.x, position.y);
            if (data == null) return;

            //�ϰ�������Ч
            if (data.Data.type == 3) return;

            //������ʼ��
            if (startCell == null)
            {
                startCell = SetStartCell(data, 1);
                Debug.Log("������ʼ��");
                RefreshAllCellUI();
                return;
            }

            //���ý�����
            if (endCell == null)
            {
                endCell = SetStartCell(data, 2);
                RefreshAllCellUI();
                Debug.Log("���ý�����");

                //�Ѿ������˿�ʼ������㣬��ʼѰ·
                if (startCell != null && endCell != null)
                {
                    Debug.Log("��ʼѰ·");
                    InitializePathFinding();
                }

                return;
            }
        }

        /// <summary>
        /// ���ÿ�ʼ�ڵ�
        /// </summary>
        /// <param name="clickItem"></param>
        private AStarGridCellData SetStartCell(AStarGridItemUI clickItem, int type)
        {
            var data = clickItem.Data;
            data.type = type;
            return data;
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ˢ�����е�Ԫ��
        /// </summary>
        private void RefreshAllCellUI()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var ui = gridUIArray.GetData(i, j);
                    ui.RefreshData();
                }
            }
        }
        #endregion

        #region �㷨���

        private List<AStarGridCellData> openList = new List<AStarGridCellData>();
        private HashSet<AStarGridCellData> closeList = new HashSet<AStarGridCellData>();

        //��Ԫ��˸�����
        private List<Vector2Int> cellNearAdjustmentList = new List<Vector2Int>()
        {
             new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1),
             new Vector2Int(-1,0), new Vector2Int(1,0),
             new Vector2Int(-1,-1), new Vector2Int(0,-1), new Vector2Int(1,-1),
        };

        /// <summary>
        /// ��ʼ��Ѱ·
        /// </summary>
        private void InitializePathFinding()
        {
            openList.Add(startCell);
            StartCoroutine(InitializePathFindingAsync());
        }

        /// <summary>
        /// �첽Ѱ·
        /// </summary>
        private IEnumerator InitializePathFindingAsync()
        {
            yield return null;

            while (openList.Count > 0)
            {
                yield return new WaitForSeconds(2);

                var curCell = GetNearestCell(openList);

                //��ǰ�ڵ���Ҫ�ӿ��Žڵ���ɾ��
                openList.Remove(curCell);
                //��ǰ�ڵ�������Ŀ���غ�
                if (curCell == endCell)
                {
                    Debug.Log("����Ŀ��");
                    var cell = CreatePath(endCell);
                    PrintPath(cell);
                    break;
                }

                //����ǰ�ڵ����ر��б���
                closeList.Add(curCell);

                curCell.isSearchingCell = true;
                //����һ��Ѱ·����
                OneStep(curCell);
                curCell.isClosedCell = true;

                RefreshAllCellUI();
            }

            yield return null;
        }

        /// <summary>
        /// һ�β���
        /// </summary>
        private void OneStep(AStarGridCellData curCell)
        {
            //�жϵ�ǰ�ڵ�8������,������뵽���Žڵ���
            var nearList = GetNearCell(curCell);

            //����Ԫ�ص�g��h��fֵ
            foreach (var nearCell in nearList)
            {
                if (closeList.Contains(nearCell))//�����ѹرյĺ��ϰ���
                    continue;

                //�ϰ���Ѱ·��Ȩ
                int obstacleDistance = 0;
                if (nearCell.type == 3)
                    obstacleDistance = AStarGridCellData.ObstacleDistance;

                var newCostToNeighbour = curCell.gCost + GetCellDistance(nearCell, curCell) + obstacleDistance;
                var isValidNeighbourNodeInOpenList = openList.Contains(nearCell);

                //�ҵ��µ�gcost��С�Ľڵ㣬��ǰ�ڵ㲻�����뿪���б���������g,hֵ
                if (!isValidNeighbourNodeInOpenList)
                {
                    nearCell.gCost = newCostToNeighbour;
                    nearCell.hCost = GetCellDistance(nearCell, endCell);
                    nearCell.parentNode = curCell;//���û��ݽڵ�

                    if (!isValidNeighbourNodeInOpenList)
                    {
                        openList.Add(nearCell);
                    }
                }
            }
            RefreshAllCellUI();
        }

        /// <summary>
        /// ��ȡF��ֵ��С�Ľڵ�
        /// </summary>
        private AStarGridCellData GetNearestCell(List<AStarGridCellData> openList)
        {
            //���ڽڵ�ʵ����IComparable����ֱ��ʹ��sort�����ȡ��һ��
            openList.Sort();
            return openList.FirstOrDefault();
        }

        /// <summary>
        /// ��ȡ�����ڵ�ľ���
        /// </summary>
        private float GetCellDistance(AStarGridCellData originCell, AStarGridCellData targetCell)
        {
            var distance = Math.Round(Vector2Int.Distance(originCell.position, targetCell.position), 1);
            return (float)distance;
        }

        /// <summary>
        /// ��ȡ�ٽ��Ľڵ㡾��Χ8����Ԫ��
        /// </summary>
        /// <param name="cell"></param>
        private List<AStarGridCellData> GetNearCell(AStarGridCellData cell)
        {
            List<AStarGridCellData> nearList = new List<AStarGridCellData>();

            foreach (var adjustment in cellNearAdjustmentList)
            {
                int x = cell.position.x + adjustment.x;
                int y = cell.position.y + adjustment.y;

                if (!IsValidCellPosition(new Vector2Int(x, y)))
                    continue;

                var data = gridDataArray.GetData(x, y);
                //��Ҫ�����ϰ���
                if (data != null && data.type != 3)
                    nearList.Add(data);
            }

            return nearList;
        }

        /// <summary>
        /// ����·��
        /// </summary>
        /// <returns></returns>
        private Stack<AStarGridCellData> CreatePath(AStarGridCellData node)
        {
            Stack<AStarGridCellData> result = new Stack<AStarGridCellData>();

            AStarGridCellData cur = node;
            while (cur.parentNode != null)
            {
                result.Push(cur);
                cur = cur.parentNode;
            }
            result.Push(cur);

            return result;
        }

        /// <summary>
        /// ��ӡ·��
        /// </summary>
        private void PrintPath(Stack<AStarGridCellData> stack)
        {
            List<Vector2Int> list = new List<Vector2Int>();
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                list.Add(item.position);
            }
            Debug.Log(string.Join("-", list));
        }
        #endregion
    }
}