using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Common.Grids
{
    public class GirdObjectPlace : BaseRenderGrid
    {
        [SerializeField] private Building[] buildingList;
        private Building curBuildPrefab;
        private Building curBuild;

        private int[,] gridPlace;//�ѷ��������λ��

        #region Initialize
        protected override void Awake()
        {
            gridPlace = new int[row, col];

            base.Awake();
            InitializeBuild();
        }

        private void InitializeBuild()
        {
            curBuildPrefab = buildingList.FirstOrDefault();
            curBuild = Instantiate(curBuildPrefab);
        }
        #endregion

        protected override void Update()
        {
            base.Update();
            SwitchBuilding();
            var cellPosition = PrePlaceBuild();
            PlaceBuild(cellPosition);
        }

        #region ��������ѡ��

        /// <summary>
        /// �л�����
        /// </summary>
        private void SwitchBuilding()
        {
            bool isChanged = false;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                curBuildPrefab = buildingList[0];
                isChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                curBuildPrefab = buildingList[1];
                isChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                curBuildPrefab = buildingList[2];
                isChanged = true;
            }

            if (isChanged)
            {
                if (curBuild != null)
                {
                    Destroy(curBuild.gameObject);
                }
                curBuild = null;
                curBuild = Instantiate(curBuildPrefab);
            }
        }

        /// <summary>
        /// Ԥ���ý���
        /// </summary>
        private Vector2Int PrePlaceBuild()
        {
            var cellPosition = GetMouseCellPosition();
            bool bl = IsValidCellPosition(cellPosition);
            if (!bl) return new Vector2Int(-1, -1);

            if (curBuild != null)
                curBuild.transform.position = GetGridWorldPostion(cellPosition);

            return cellPosition;
        }

        #endregion

        #region ��������

        private bool CanBuild(Vector2Int clickCellPosition)
        {
            var size = curBuild.size;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var position = new Vector2Int(clickCellPosition.x + j, clickCellPosition.y + i);
                    if (gridPlace[position.x, position.y] == 1)
                    {
                        Debug.Log("�ؿ��Ѿ���ռ��");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        private void PlaceBuild(Vector2Int clickCellPosition)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!IsValidCellPosition(clickCellPosition) || curBuild == null || !CanBuild(clickCellPosition)) return;

            var size = curBuild.size;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var position = new Vector2Int(clickCellPosition.x + j, clickCellPosition.y + i);
                    gridPlace[position.x, position.y] = 1;
                }
            }
            //�ڵ�ǰλ������һ������
            var initializePosition = GetGridWorldPostion(clickCellPosition);
            Instantiate(curBuildPrefab, initializePosition, Quaternion.identity);
        }
        #endregion

        #region ���߷���
        /// <summary>
        /// ��ȡ��ǰ��Ԫ�����������
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected Vector2 GetGridWorldPostion(Vector2Int position)
        {
            ////�����Ķ�λ��������
            //return new Vector2(position.x * gridWidth + gridWidth / 2, position.y * gridWidth + gridWidth / 2);
            //�����Ķ�λ�������½�
            return new Vector2(position.x * gridWidth, position.y * gridWidth);
        }
        #endregion
    }
}