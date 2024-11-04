using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using static UnityEditor.Progress;

namespace Demo.UI
{
    public class CircleItemController : MonoBehaviour
    {
        [SerializeField] private GameObject ItemPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private float radius = 100;
        [SerializeField] private float yOffest = 30;
        [SerializeField] private int ItemCount = 9;


        private List<CircleViewItemModel> itemViewList = new List<CircleViewItemModel>();
        private List<CircleItemPosition> dataList;

        private void Start()
        {
            //特别注意：SiblingIndex在Awake中设置无效
            Initialize();
        }

        #region 生成位置对象

        private void Initialize()
        {
            dataList = CreatePositionItemList();

            int maxDepth = dataList.Max(p => p.Depth);

            //生成预制体对象
            foreach (var item in dataList)
            {
                var itemView = Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity, parent);
                itemViewList.Add(new CircleViewItemModel(itemView, item));

                RefrestUI(itemView, item);
            }
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        private List<CircleItemPosition> CreatePositionItemList()
        {
            List<CircleItemPosition> result = new List<CircleItemPosition>();

            int remainCount = ItemCount - 1;
            int depth = 0;
            int index = 0;

            //中心区域位置，特殊处理
            CircleItemPosition item = new CircleItemPosition(depth++, index++);
            result.Add(item);

            //后面位置
            while (remainCount > 0)
            {
                if (remainCount >= 2)
                {
                    remainCount -= 2;
                    result.Add(new CircleItemPosition(depth, index));//左边
                    result.Add(new CircleItemPosition(depth, ItemCount - index));//右边
                    index++;
                }
                else
                {
                    remainCount -= 1;
                    result.Add(new CircleItemPosition(depth, index++));
                }
                depth++;
            }

            ////设置SiblingIndex，最前面的值最大,所以从深的元素开始设置
            ////这样设置最简单
            //int siblingIndex = 0;
            //while (depth >= 0)
            //{
            //    result.Where(p => p.Depth == depth).ToList().ForEach(p =>
            //    {
            //        p.SiblingIndex = siblingIndex++;
            //    });
            //    depth--;
            //}

            //初始化数据
            result.ForEach(p => p.InitData(ItemCount, new Vector3(0, -100, 0), radius: radius, yInterval: yOffest));

            return result.OrderBy(p => p.Index).ToList();
        }

        #endregion

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "左转"))
            {
                TurnLeft();
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "右转"))
            {
                TurnRight();
            }
        }

        private void TurnLeft()
        {
            int maxIndex = dataList.Max(p => p.Index);
            for (int i = 0; i < itemViewList.Count; i++)
            {
                var itemView = itemViewList[i];

                int nextIndex = (itemView.Data.Index - 1) >= 0 ? itemView.Data.Index - 1 : maxIndex;
                var nextData = dataList.FirstOrDefault(p => p.Index == nextIndex);

                //移动
                itemView.SetData(nextData);

                RefrestUI(itemView.Item, nextData);

            }
        }

        private void TurnRight()
        {
            for (int i = 0; i < itemViewList.Count; i++)
            {
                var itemView = itemViewList[i];

                int nextIndex = (itemView.Data.Index + 1) % dataList.Count;
                var nextData = dataList.FirstOrDefault(p => p.Index == nextIndex);

                //移动
                itemView.SetData(nextData);

                RefrestUI(itemView.Item, nextData);

            }
        }

        private void RefrestUI(GameObject itemView, CircleItemPosition data)
        {
            TextMeshProUGUI textMesh = itemView.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh)
                textMesh.text = $"{data.Index},S:{data.SiblingIndex},D:{data.Depth}";
        }
        #region Inner Class

        /// <summary>
        /// 环形物品对象
        /// </summary>
        private class CircleViewItemModel
        {
            public CircleViewItemModel(GameObject item, CircleItemPosition data)
            {
                Item = item;
                SetData(data);
            }

            public GameObject Item { get; set; }

            public CircleItemPosition Data { get; set; }

            public void SetData(CircleItemPosition data)
            {
                Data = data;

                var rect = Item.GetComponent<RectTransform>();
                rect.DOLocalMove(data.Position, 1);
                rect.DOScale(data.Scale, 1);
                rect.SetSiblingIndex(this.Data.SiblingIndex);

                Item.name = this.Data.SiblingIndex.ToString();

                //Debug.Log($"Index:{this.Data.Index},Depth:{this.Data.Depth},SiblingIndex:{this.Data.SiblingIndex}");
            }
        }

        #endregion
    }
}
