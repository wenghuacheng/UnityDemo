using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Games.MineSweep
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private SpriteRenderer blankSprite;//Ĭ�Ͽհױ���
        [SerializeField] private SpriteRenderer searchedSprite;//����������
        [SerializeField] private SpriteRenderer mineSprite;//������ͼ

        /// <summary>
        /// ��Ԫ��״̬��0:δ��ʾ��1���ѽ�ʾ��
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// �Ƿ��ǵ��׵�Ԫ
        /// </summary>
        public bool isMineCell { get; set; }

        /// <summary>
        ///  UIˢ��
        /// </summary>
        public void SetStatus(int status)
        {
            Status = status;

            if (status == 0)
            {
                //δ��ʾ
                text.gameObject.SetActive(false);
                searchedSprite.gameObject.SetActive(false);
                mineSprite.gameObject.SetActive(false);
                blankSprite.gameObject.SetActive(true);
            }
            else if (status == 1)
            {
                //�ѽ�ʾ
                if (isMineCell)
                {
                    //���׵�Ԫ
                    text.gameObject.SetActive(false);
                    searchedSprite.gameObject.SetActive(true);
                    mineSprite.gameObject.SetActive(true);
                    blankSprite.gameObject.SetActive(false);
                }
                else
                {
                    //��ͨ��Ԫ
                    text.gameObject.SetActive(false);
                    searchedSprite.gameObject.SetActive(true);
                    mineSprite.gameObject.SetActive(false);
                    blankSprite.gameObject.SetActive(false);
                }
            }

            ////����
            //if (isMineCell)
            //    mineSprite.gameObject.SetActive(true);
        }

        /// <summary>
        /// ���õ�������
        /// </summary>
        /// <param name="count"></param>
        public void SetMineCount(int count)
        {
            if (!isMineCell && count > 0) text.gameObject.SetActive(true);
            text.text = count.ToString();
        }
    }
}