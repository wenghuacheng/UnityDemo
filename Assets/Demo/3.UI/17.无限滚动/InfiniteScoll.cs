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
     Content��Ҫ���Content Size Fitter���������Ա��ſ��������϶�ʱ���Զ��ص�����һ��
     */


    public class InfiniteScoll : MonoBehaviour
    {
        //������ʾ��Ŀ
        private List<string> contentList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
        //��ǰ���е���Ŀ
        private List<InfiniteScollItem> infiniteScollList = new List<InfiniteScollItem>();

        //��ǰ��ʾ
        private int currentContentIndex = 0;
        //��ҳ���
        private float totalPageWidth = 0;
        private float itemWidth = 0;
        private float itemSpacing = 0;
        //�����ٶ�
        private float scollSpeed = 100;

        [SerializeField] private HorizontalLayoutGroup horizontalLayout;
        //�ܹ���������һҳ����Ŀ
        [SerializeField] private List<RectTransform> itemObjects;
        [SerializeField] private RectTransform contentRect;
        [SerializeField] private ScrollRect scrollView;

        private void Start()
        {
            var firstItem = itemObjects.FirstOrDefault();

            //�������������ʾ���ܳ���
            itemWidth = firstItem.GetComponent<RectTransform>().rect.width;
            itemSpacing = horizontalLayout.spacing;

            totalPageWidth = (itemWidth + itemSpacing) * itemObjects.Count;

            foreach (var item in itemObjects)
            {
                infiniteScollList.Add(item.GetComponent<InfiniteScollItem>());
            }

            //����ѡ������������ݣ�ֹͣʱ�Ͳ��ῼ��Ԫ��λ�Ƶ�ͷ�������鷳�����⣬ֱ������ۼƼ���
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


        #region ��齱Ч��
        float time = 0;
        private void ControlScoll()
        {
            time += Time.deltaTime;
            if (time <= 3)
            {
                //3���ڿ��ٹ���
                scollSpeed = 500;
            }
            else
            {
                int s = 5;
                //��ʼ����
                if (scollSpeed > s)
                    scollSpeed -= Time.deltaTime * 100;
                else if (scollSpeed > 0 && scollSpeed <= s)
                {
                    scollSpeed = 0;

                    //������ӽ���Ŀ���ƶ�
                    float deltaWidth = scrollView.GetComponent<RectTransform>().rect.width / 2;
                    //ע�⣺���㵱ǰλ��ʱ��Ҫ���ǣ��齱��־���м䣬��Ҫ����һ��Ľ��������
                    var x = Mathf.Abs(contentRect.localPosition.x) + deltaWidth;
                    //�ڵ�ǰ��������ʾ��λ��+1����ΪĿ��λ��
                    int count = (int)(x / (itemWidth + itemSpacing)) + 1;

                    //ʵ���Ǵӵ�һ��Ԫ�ؿ�ʼ�ģ��������cout�Ǽ����˽�������ȵ�һ�룬�ڼ���ʵ��λ��ʱ��Ҫȥ��
                    //�ӵ�ǰλ������ƶ����Ԫ�صĿ�ȣ�������ָ�����м�
                    var target = (itemWidth + itemSpacing) * count - deltaWidth + itemWidth / 2;
                    contentRect.DOLocalMoveX(-target, 1f);

                    //Debug.Log($"cout:{count},target:{target}");
                    Debug.Log(infiniteScollList[count].GetText());
                }
            }
        }

        #endregion


        /// <summary>
        /// �Զ�����
        /// </summary>
        private void AutoScollContent()
        {
            //Canvas.ForceUpdateCanvases();
            if (scollSpeed > 0)
            {
                //�����ƶ�ʱ����X
                contentRect.localPosition -= new Vector3(scollSpeed * Time.deltaTime, 0, 0);

                if (contentRect.localPosition.x <= -totalPageWidth)
                {
                    contentRect.localPosition += new Vector3(totalPageWidth, 0, 0);

                    //�������ı�ʱ����ʼ�Ŀؼ�����ˢ��
                    //�磺������ǰһҳ���ı�ΪA,B,C,D����ˢ��λ��ʱ��A->E,B->F����������
                    currentContentIndex += itemObjects.Count;
                    RefreshText();
                }
            }
        }

        /// <summary>
        /// ˢ����ʾ����ı�
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