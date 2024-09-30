using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids.BattleFlagDemo
{
    public class Player : MonoBehaviour
    {
        private Camera mainCamera;
        //��������
        private List<GameObject> bgGridList = new List<GameObject>();

        void Start()
        {
            mainCamera = Camera.main;
            //���������������ڵ������
            DrawAreaGridBg();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var posIndex = CalGridPosIndex(worldPosition);
                GenerateAvailableArea(posIndex);
            }
        }

        #region �������
        private int gridSize = 5;
        private Vector2 centerPointPos = Vector2.zero;//�������ĵ�(0,0)

        /// <summary>
        /// ���ɿɻ����
        /// </summary>
        /// <param name="posIndex"></param>
        private void GenerateAvailableArea(Vector2 posIndex)
        {
            var posIndexList = CalAvailableArea(posIndex);
            DrawGridBg(posIndexList);
        }

        /// <summary>
        /// ������������ڵ�Ԫ��
        /// </summary>
        private Vector2 CalGridPosIndex(Vector2 worldPosition)
        {
            var pos = worldPosition - centerPointPos;//�����������

            //���������ĵ㶨λ�����Ա߽���Ҫ����λ��
            float xOffest = worldPosition.x < 0 ? -gridSize / 2f : gridSize / 2f;
            float yOffest = worldPosition.y < 0 ? -gridSize / 2f : gridSize / 2f;

            var x = (int)((worldPosition.x + xOffest) / gridSize);
            var y = (int)((worldPosition.y + yOffest) / gridSize);

            //Debug.Log($"���꣺{pos},λ������:{new Vector2(x, y)},xOffest:{xOffest}");

            return new Vector2(x, y);
        }

        /// <summary>
        /// ����ɻ����
        /// </summary>
        private List<Vector2> CalAvailableArea(Vector2 posIndex)
        {
            int length = 3;

            List<Vector2> list = new List<Vector2>();
            for (int x = -length; x <= length; x++)
            {
                for (int y = -length; y <= length; y++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) <= length)
                    {
                        Vector2 p = posIndex + new Vector2(x, y);
                        if (!list.Contains(p))
                            list.Add(p);
                    }
                }
            }

            return list;
        }

        #region ���Ʊ�������
        [SerializeField] private GameObject gridPrefab;
        /// <summary>
        /// ����һ�����������(���ڵ������)
        /// </summary>
        private void DrawAreaGridBg()
        {
            DestroyGridBg();

            for (int i = -10; i <= 10; i++)
            {
                for (int j = -10; j <= 10; j++)
                {
                    Vector2 pos = new Vector2(i * gridSize, j * gridSize);
                    var obj = Instantiate(gridPrefab, pos, Quaternion.identity, this.transform);
                    obj.transform.localScale = new Vector3(gridSize, gridSize, 1);
                    obj.GetComponent<GridBg>().SetIndex(new Vector2(i, j));
                    bgGridList.Add(obj);
                }
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        private void DrawGridBg(List<Vector2> posIndexList)
        {
            DestroyGridBg();

            foreach (var index in posIndexList)
            {
                Vector2 pos = new Vector2(index.x * gridSize, index.y * gridSize);
                var obj = Instantiate(gridPrefab, pos, Quaternion.identity, this.transform);
                obj.transform.localScale = new Vector3(gridSize, gridSize, 1);
                obj.GetComponent<GridBg>().SetIndex(index);
                bgGridList.Add(obj);
            }
        }

        /// <summary>
        /// ���ٱ�������
        /// </summary>
        private void DestroyGridBg()
        {
            foreach (var item in bgGridList)
            {
                Destroy(item);
            }
        }
        #endregion

        #endregion
    }
}