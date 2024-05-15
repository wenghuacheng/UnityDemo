using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Games.MineSweep
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private SpriteRenderer blankSprite;//默认空白背景
        [SerializeField] private SpriteRenderer searchedSprite;//已搜索背景
        [SerializeField] private SpriteRenderer mineSprite;//地雷贴图

        /// <summary>
        /// 单元格状态（0:未揭示，1：已揭示）
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// 是否是地雷单元
        /// </summary>
        public bool isMineCell { get; set; }

        /// <summary>
        ///  UI刷新
        /// </summary>
        public void SetStatus(int status)
        {
            Status = status;

            if (status == 0)
            {
                //未揭示
                text.gameObject.SetActive(false);
                searchedSprite.gameObject.SetActive(false);
                mineSprite.gameObject.SetActive(false);
                blankSprite.gameObject.SetActive(true);
            }
            else if (status == 1)
            {
                //已揭示
                if (isMineCell)
                {
                    //地雷单元
                    text.gameObject.SetActive(false);
                    searchedSprite.gameObject.SetActive(true);
                    mineSprite.gameObject.SetActive(true);
                    blankSprite.gameObject.SetActive(false);
                }
                else
                {
                    //普通单元
                    text.gameObject.SetActive(false);
                    searchedSprite.gameObject.SetActive(true);
                    mineSprite.gameObject.SetActive(false);
                    blankSprite.gameObject.SetActive(false);
                }
            }

            ////测试
            //if (isMineCell)
            //    mineSprite.gameObject.SetActive(true);
        }

        /// <summary>
        /// 设置地雷数量
        /// </summary>
        /// <param name="count"></param>
        public void SetMineCount(int count)
        {
            if (!isMineCell && count > 0) text.gameObject.SetActive(true);
            text.text = count.ToString();
        }
    }
}