using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /**
     * 思路
     * 基于数量通过平方根计算出行列
     * 基于设置固定行，还是固定列等调整行列的数量
     * 基于Padding，间距等信息计算出单个元素的宽高。如果是用户固定尺寸则优先使用用户的尺寸设置
     * 通过行列数量排布元素的位置
     */

    public class FlexibleGridLayout : LayoutGroup
    {
        //排列方式
        public enum FitType
        {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns,
        }

        public int rows;
        public int columns;
        public Vector2 cellSize;
        public Vector2 spacing;
        public FitType fitType;

        public bool fixX;
        public bool fixY;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            //基于平方根计算行列
            if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
            {
                fixX = true;
                fixY = true;
                float sqrRt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }
            //宽度适配，则平方根计算列，行由列数计算
            if (fitType == FitType.Width || fitType == FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }
            //高度适配，则平方根计算行，列由行数计算
            if (fitType == FitType.Height || fitType == FitType.FixedRows)
            {

                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            //通过间距，Padding等计算出单个元素的尺寸
            float cellWidth = (parentWidth / (float)columns) - (spacing.x * (columns - 1)) / (float)columns - (padding.left / (float)columns) - (padding.right / (float)columns);
            float cellHeight = (parentHeight / (float)rows) - (spacing.y * (rows - 1)) / (float)rows - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            //判断是否使用用户设置的尺寸
            cellSize.x = fixX ? cellWidth : cellSize.x;
            cellSize.y = fixY ? cellHeight : cellSize.y;

            //计算元素的位置并进行设置
            int columnCount = 0;
            int rowCount = 0;
            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = cellSize.x * columnCount + spacing.x * columnCount + padding.left;
                var yPos = cellSize.y * rowCount + spacing.y * rowCount + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }

        }

        public override void CalculateLayoutInputVertical()
        {

        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
    }
}