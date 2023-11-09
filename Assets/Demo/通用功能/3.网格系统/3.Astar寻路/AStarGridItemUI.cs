using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.Grids
{
    public class AStarGridItemUI : MonoBehaviour
    {
        //总距离
        [SerializeField] private TextMeshProUGUI fText;
        //起点距离
        [SerializeField] private TextMeshProUGUI gText;
        //终点距离
        [SerializeField] private TextMeshProUGUI hText;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public AStarGridCellData Data { get; set; }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            if (this.Data == null) return;
            SetCellColor();

            int type = Data.type;
            if (type == 0 || type == 1 || type == 2)
                SetData(Data);
            else
                EmptyData();
        }

        /// <summary>
        /// 设置单元格颜色
        /// </summary>
        /// <param name="type"></param>
        private void SetCellColor()
        {
            int type = Data.type;

            if (type == 0)
            {
                spriteRenderer.color = Color.gray;
            }
            else if (type == 1)
            {
                spriteRenderer.color = Color.red;
            }
            else if (type == 2)
            {
                spriteRenderer.color = Color.blue;
            }
            else if (type == 3)
            {
                spriteRenderer.color = Color.black;
            }

            if (Data.isSearchingCell)
            {
                spriteRenderer.color = Color.yellow;
            }
            else if (Data.isClosedCell)
            {
                spriteRenderer.color = Color.green;
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        private void SetData(AStarGridCellData data)
        {
            gText.text = data.gCost.ToString();
            hText.text = data.hCost.ToString();
            fText.text = data.fCost.ToString();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void EmptyData()
        {
            gText.text = "";
            hText.text = "";
            fText.text = "";
        }
    }
}