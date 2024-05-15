using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.GoGrid
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private int cellCount = 40;//��Ԫ������
        [SerializeField] private int maxHorizontalCell = 11;//����ˮƽ��Ԫ������
        [SerializeField] private int maxVerticalCell = 2;//���Ĵ�ֱ��Ԫ������
        [SerializeField] private Cell cellPrefab;//�ؿ�Ԥ����

        private float cellSize = 5;//��Ԫ��ߴ� 
        private List<Vector3> cellPosList = new List<Vector3>();//��Ԫ��λ���б�
        private List<Cell> cellVisualList = new List<Cell>();//��Ԫ����ʾԪ���б�

        #region ��ʼ��
        private void Awake()
        {
            InitializeGrid();
            CenterMap();
            InitializeGridVisual();
            RotateDirection();

            //StartCoroutine(AutoSwitchCellColor());
        }

        /// <summary>
        /// ���������λ��
        /// </summary>
        private void InitializeGrid()
        {
            int remainCount = cellCount;
            int curHorizontalIndex = 0;//��ǰˮƽ������������
            int curVerticalIndex = 0;//��ǰ��ֱ������������

            int stepVerticalCount = 0;//һ���׶������ɴ�ֱ�����������

            int index = 0;//���ɷ���������0�����ң�1���£�2��������3����

            while (remainCount > 0)
            {
                Vector2 pos = new Vector2(curHorizontalIndex * cellSize, -curVerticalIndex * cellSize);
                cellPosList.Add(pos);

                remainCount--;

                //������һ��λ��
                index = index % 4;//��4����Ϊһ��ѭ��
                if (index == 0)//������������
                {
                    #region ������������
                    if (curHorizontalIndex >= maxHorizontalCell)
                    {
                        //����
                        curVerticalIndex++;
                        index++;
                        stepVerticalCount = 0;
                    }
                    else
                    {
                        //������չ
                        curHorizontalIndex++;
                    }
                    #endregion
                }
                else if (index == 1 || index == 3)//��������
                {
                    #region ��������
                    if (stepVerticalCount >= maxVerticalCell - 1)
                    {
                        index++;
                    }
                    curVerticalIndex++;
                    stepVerticalCount++;
                    #endregion
                }
                else if (index == 2)//������������
                {
                    #region ������������
                    if (curHorizontalIndex <= 0)
                    {
                        //����
                        curVerticalIndex++;
                        index++;
                        stepVerticalCount = 0;
                    }
                    else
                    {
                        //������չ
                        curHorizontalIndex--;
                    }
                    #endregion
                }

            }
        }

        /// <summary>
        /// ���������λ��
        /// </summary>
        private void InitializeGridVisual()
        {
            for (int i = 0; i < cellPosList.Count; i++)
            {
                var obj = Instantiate(cellPrefab, cellPosList[i], Quaternion.identity, this.transform);
                obj.transform.localScale = new Vector3(cellSize * 0.9f, cellSize * 0.9f, 1);//��Сһ�㣬������
                cellVisualList.Add(obj);
            }
        }

        /// <summary>
        /// �õ�ͼ����
        /// </summary>
        private void CenterMap()
        {
            var minX = cellPosList.Select(p => p.x).Min();
            var maxX = cellPosList.Select(p => p.x).Max();
            var minY = cellPosList.Select(p => p.y).Min();
            var maxY = cellPosList.Select(p => p.y).Max();

            var offest = new Vector3(Mathf.Abs((maxX - minX) / 2), -Mathf.Abs((maxY - minY) / 2));

            for (int i = 0; i < cellPosList.Count; i++)
            {
                cellPosList[i] = cellPosList[i] - offest;
            }
        }

        /// <summary>
        /// ��תָʾ����
        /// </summary>
        private void RotateDirection()
        {
            for (int i = 1; i < cellVisualList.Count; i++)
            {
                var prevObj = cellVisualList[i - 1];
                var curObj = cellVisualList[i];

                //��ת�ؿ飬�ü�ͷָʾ��һ��λ��
                var direction = (curObj.transform.position - prevObj.transform.position).normalized;
                prevObj.transform.up = direction;
            }

            //���һ��ʹ��ǰ�����ת����
            cellVisualList[cellVisualList.Count - 1].transform.up = cellVisualList[cellVisualList.Count - 2].transform.up;
        }
        #endregion

        #region ����
        public List<Vector3> CellPositionList { get { return cellPosList; } }
        #endregion

        /// <summary>
        /// �л���Ԫ�����ɫ
        /// </summary>
        private IEnumerator AutoSwitchCellColor()
        {
            int curIndex = 0;
            int prevIndex = 0;
            while (true)
            {
                cellVisualList[prevIndex].SetStatus(0);
                cellVisualList[curIndex].SetStatus(1);

                yield return new WaitForSeconds(0.5f);

                prevIndex = curIndex;
                curIndex = (curIndex + 1) % cellVisualList.Count;
            }
        }
    }
}