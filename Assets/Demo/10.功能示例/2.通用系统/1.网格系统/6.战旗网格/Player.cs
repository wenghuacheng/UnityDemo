using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Grids.BattleFlagDemo
{
    public class Player : MonoBehaviour
    {
        private Camera mainCamera;
        //背景网格
        private List<GameObject> bgGridList = new List<GameObject>();

        void Start()
        {
            mainCamera = Camera.main;
            //绘制整个区域用于点击测试
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

        #region 网格相关
        private int gridSize = 5;
        private Vector2 centerPointPos = Vector2.zero;//网格中心点(0,0)

        /// <summary>
        /// 生成可活动区域
        /// </summary>
        /// <param name="posIndex"></param>
        private void GenerateAvailableArea(Vector2 posIndex)
        {
            var posIndexList = CalAvailableArea(posIndex);
            DrawGridBg(posIndexList);
        }

        /// <summary>
        /// 计算坐标点所在单元格
        /// </summary>
        private Vector2 CalGridPosIndex(Vector2 worldPosition)
        {
            var pos = worldPosition - centerPointPos;//矫正后的坐标

            //由于是中心点定位，所以边界需要进行位移
            float xOffest = worldPosition.x < 0 ? -gridSize / 2f : gridSize / 2f;
            float yOffest = worldPosition.y < 0 ? -gridSize / 2f : gridSize / 2f;

            var x = (int)((worldPosition.x + xOffest) / gridSize);
            var y = (int)((worldPosition.y + yOffest) / gridSize);

            //Debug.Log($"坐标：{pos},位置索引:{new Vector2(x, y)},xOffest:{xOffest}");

            return new Vector2(x, y);
        }

        /// <summary>
        /// 计算可活动区域
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

        #region 绘制背景网格
        [SerializeField] private GameObject gridPrefab;
        /// <summary>
        /// 绘制一个区域的网格(用于点击测试)
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
        /// 绘制网格
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
        /// 销毁背景网格
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