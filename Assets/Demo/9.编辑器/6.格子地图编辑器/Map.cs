using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.CustomEditors
{
#if UNITY_EDITOR
    public class Map : MonoBehaviour
    {

        [SerializeField] private CustomLandMapCanvas canvas;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private GameObject cellPrefab;

        private int gridSize = 10;

        #region ≥ı ºªØ

        private void Awake()
        {
            InitilizeMap();
        }

        private void InitilizeMap()
        {
            var nodeList = canvas.NodeList;

            for (int i = 0; i < CustomLandMapCanvas.row; i++)
            {
                for (int j = 0; j < CustomLandMapCanvas.col; j++)
                {
                    var pos = new Vector2(i * gridSize, j * gridSize);

                    var node = nodeList.FirstOrDefault(p => p.commonGridPosition.x == i && p.commonGridPosition.y == j);
                    if (node == null)
                    {
                        var obj = Instantiate(emptyCellPrefab, pos, Quaternion.identity, this.transform);
                        obj.transform.localScale = new Vector3(gridSize, gridSize, 1);
                    }
                    else
                    {
                        Debug.Log(i + "-" + j);
                        var obj = Instantiate(cellPrefab, pos, Quaternion.identity, this.transform);
                        obj.transform.localScale = new Vector3(gridSize, gridSize, 1);
                    }

                }
            }

            foreach (var item in nodeList)
            {
                Debug.Log(item.ToString());
            }
        }
        #endregion

        void Update()
        {

        }
    }
#endif
}