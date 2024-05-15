using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Demo.UI
{
    /***
     Content需要添加Content Size Fitter让容器可以被撑开，否则拖动时会自动回弹到第一个
     */


    public class InfiniteScoll : MonoBehaviour
    {
        //内容显示项目
        private List<string> contentList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
        //当前所有的项目
        private List<InfiniteScollItem> infiniteScollList = new List<InfiniteScollItem>();

        //当前显示
        private int currentContentIndex = 0;
        //整页宽度
        private float totalPageWidth = 0;
        private float itemWidth = 0;
        private float itemSpacing = 0;
        //滚动速度
        private float scollSpeed = 100;

        [SerializeField] private HorizontalLayoutGroup horizontalLayout;
        //能够填充进度条一页的项目
        [SerializeField] private List<RectTransform> itemObjects;
        [SerializeField] private RectTransform contentRect;
        [SerializeField] private ScrollRect scrollView;

        private void Start()
        {
            var firstItem = itemObjects.FirstOrDefault();

            //计算滚动条中显示的总长度
            itemWidth = firstItem.GetComponent<RectTransform>().rect.width;
            itemSpacing = horizontalLayout.spacing;

            totalPageWidth = (itemWidth + itemSpacing) * itemObjects.Count;

            foreach (var item in itemObjects)
            {
                infiniteScollList.Add(item.GetComponent<InfiniteScollItem>());
            }

            //可以选择添加两组数据，停止时就不会考虑元素位移到头部计算麻烦的问题，直接向后累计即可
            //for (int i = 0; i < 2; i++)
            {
                foreach (var item in itemObjects)
                {
                    RectTransform newItem = Instantiate(item, Vector3.zero, Quaternion.identity, horizontalLayout.transform);
                    newItem.SetAsLastSibling();
                    infiniteScollList.Add(newItem.GetComponent<InfiniteScollItem>());
                }
            }

            RefreshText();
        }

        void Update()
        {
            ControlScoll();
            AutoScollContent();
        }


        #region 类抽奖效果
        float time = 0;
        private void ControlScoll()
        {
            time += Time.deltaTime;
            if (time <= 3)
            {
                //3秒内快速滚动
                scollSpeed = 500;
            }
            else
            {
                int s = 5;
                //开始减速
                if (scollSpeed > s)
                    scollSpeed -= Time.deltaTime * 100;
                else if (scollSpeed > 0 && scollSpeed <= s)
                {
                    scollSpeed = 0;

                    //向着最接近的目标移动
                    float deltaWidth = scrollView.GetComponent<RectTransform>().rect.width / 2;
                    //注意：计算当前位数时需要考虑，抽奖标志在中间，需要加上一半的进度条宽度
                    var x = Mathf.Abs(contentRect.localPosition.x) + deltaWidth;
                    //在当前进度条显示的位置+1，作为目标位置
                    int count = (int)(x / (itemWidth + itemSpacing)) + 1;

                    //实际是从第一个元素开始的，而这里的cout是加上了进度条宽度的一半，在计算实际位移时需要去掉
                    //从当前位置向后移动半个元素的宽度，即可让指针在中间
                    var target = (itemWidth + itemSpacing) * count - deltaWidth + itemWidth / 2;
                    contentRect.DOLocalMoveX(-target, 1f);

                    //Debug.Log($"cout:{count},target:{target}");
                    Debug.Log(infiniteScollList[count].GetText());
                }
            }
        }

        #endregion


        /// <summary>
        /// 自动滚动
        /// </summary>
        private void AutoScollContent()
        {
            //Canvas.ForceUpdateCanvases();
            if (scollSpeed > 0)
            {
                //向右移动时减少X
                contentRect.localPosition -= new Vector3(scollSpeed * Time.deltaTime, 0, 0);

                if (contentRect.localPosition.x <= -totalPageWidth)
                {
                    contentRect.localPosition += new Vector3(totalPageWidth, 0, 0);

                    //在重置文本时将起始的控件进行刷新
                    //如：界面上前一页的文本为A,B,C,D。在刷新位置时将A->E,B->F。依次类推
                    currentContentIndex += itemObjects.Count;
                    RefreshText();
                }
            }
        }

        /// <summary>
        /// 刷新显示项的文本
        /// </summary>
        private void RefreshText()
        {
            int index = currentContentIndex;

            foreach (var item in infiniteScollList)
            {
                index = index % contentList.Count;
                var content = contentList[index];
                item.SetText(content);
                index++;
            }
        }
    }
}