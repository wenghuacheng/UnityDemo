using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /**
     * ˼·
     * ��������ͨ��ƽ�������������
     * �������ù̶��У����ǹ̶��еȵ������е�����
     * ����Padding��������Ϣ���������Ԫ�صĿ�ߡ�������û��̶��ߴ�������ʹ���û��ĳߴ�����
     * ͨ�����������Ų�Ԫ�ص�λ��
     */

    public class FlexibleGridLayout : LayoutGroup
    {
        //���з�ʽ
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

            //����ƽ������������
            if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
            {
                fixX = true;
                fixY = true;
                float sqrRt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }
            //������䣬��ƽ���������У�������������
            if (fitType == FitType.Width || fitType == FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }
            //�߶����䣬��ƽ���������У�������������
            if (fitType == FitType.Height || fitType == FitType.FixedRows)
            {

                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            //ͨ����࣬Padding�ȼ��������Ԫ�صĳߴ�
            float cellWidth = (parentWidth / (float)columns) - (spacing.x * (columns - 1)) / (float)columns - (padding.left / (float)columns) - (padding.right / (float)columns);
            float cellHeight = (parentHeight / (float)rows) - (spacing.y * (rows - 1)) / (float)rows - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            //�ж��Ƿ�ʹ���û����õĳߴ�
            cellSize.x = fixX ? cellWidth : cellSize.x;
            cellSize.y = fixY ? cellHeight : cellSize.y;

            //����Ԫ�ص�λ�ò���������
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